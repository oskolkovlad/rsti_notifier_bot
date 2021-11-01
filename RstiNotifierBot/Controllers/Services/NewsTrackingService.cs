namespace RstiNotifierBot.Controllers.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using RstiNotifierBot.Extensions;
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;
    using RstiNotifierBot.Properties;
    using RstiNotifierBot.Model.Entities;
    using RstiNotifierBot.Dto;

    internal class NewsTrackingService : BaseService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ITelegramBotHandler _botHandler;
        private readonly IMessageHandler _messageHandler;
        private readonly IInlineMarkupHandler _inlineMarkupHandler;
        private readonly IBCNews _bCNews;
        private readonly IBCNewsList _bCNewsList;
        private readonly IBCChatProperty _bcChatProperty;
        private readonly IBCSchedulerTasks _bcSchedulerTasks;

        private readonly string _taskId;

        public NewsTrackingService(
            ITelegramBotClient botClient,
            ITelegramBotHandler botHandler,
            IMessageHandler messageHandler,
            IInlineMarkupHandler inlineMarkupHandler,
            IBCNews bCNews,
            IBCNewsList bCNewsList,
            IBCChatProperty bcChatProperty,
            IBCSchedulerTasks bcSchedulerTasks)
        {
            _botClient = botClient;
            _botHandler = botHandler;
            _messageHandler = messageHandler;
            _inlineMarkupHandler = inlineMarkupHandler;
            _bCNews = bCNews;
            _bCNewsList = bCNewsList;
            _bcChatProperty = bcChatProperty;
            _bcSchedulerTasks = bcSchedulerTasks;

            _taskId = Guid.NewGuid().ToString().Clear("-");
        }

        #region BaseService Members

        protected override void StartAction() =>
            _bcSchedulerTasks.ScheduleTask(_taskId, async () => await CheckNewsAsync());

        protected override void StopAction() => _bcSchedulerTasks.StopTask(_taskId);

        #endregion

        #region Private Members

        private async Task CheckNewsAsync()
        {
            try
            {
                var addedNewsItems = (await AddRecentNewsAsync()).ToList();
                if (addedNewsItems.Count == 0)
                {
                    return;
                }

                await SendNewsAsync(addedNewsItems);
            }
            catch (Exception exception)
            {
                exception.OutputConsoleLog();
            }
        }

        private async Task<IEnumerable<News>> AddRecentNewsAsync()
        {
            var currentLastNewsItems = (await _bCNewsList.GetNewsItemsAsync(Resources.NewsUrl)).ToList();
            if (currentLastNewsItems.Count == 0)
            {
                return currentLastNewsItems;
            }

            var addedNewsItems = new List<News>();

            var lastNewsItems = _bCNews.GetLastNewsItems().ToList();
            if (lastNewsItems.Count == 0)
            {
                foreach (var currentLastNewsItem in currentLastNewsItems)
                {
                    _bCNews.Create(currentLastNewsItem);
                    addedNewsItems.Add(currentLastNewsItem);
                }
            }
            else
            {
                foreach (var currentLastNewsItem in currentLastNewsItems)
                {
                    if (lastNewsItems.All(x => x.PublishDate != currentLastNewsItem.PublishDate &&
                        x.Url != currentLastNewsItem.Url &&
                        x.Title != currentLastNewsItem.Title))
                    {
                        _bCNews.Create(currentLastNewsItem);
                        addedNewsItems.Add(currentLastNewsItem);
                    }
                }
            }

            return addedNewsItems;
        }

        private async Task SendNewsAsync(IEnumerable<News> addedNewsItems)
        {
            var value = true.ToString().ToLower();
            var subscribedChatIds = _bcChatProperty.GetProperties(Resources.SubscriptionPropertyName, value)
                .Select(x => x.ChatId).ToList();

            var newsItems = addedNewsItems.ToList();
            foreach (var chatId in subscribedChatIds)
            {
                foreach (var newsItem in newsItems)
                {
                    await SendNewsAsync(chatId, newsItem);
                }
            }
        }

        private async Task SendNewsAsync(long chatId, News item)
        {
            var message = _messageHandler.GetNewsMessage(item);
            var inlineMarkup = _inlineMarkupHandler.GetPostReplyMarkup(item.Url);
            var post = new PostDto(message, item.ImageUrl, inlineMarkup);

            await _botHandler.MakePostAsync(_botClient, chatId, post);
        }

        #endregion
    }
}
