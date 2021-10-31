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
            _bcSchedulerTasks.ScheduleTask(_taskId, async () => await CheckNews());

        protected override void StopAction() => _bcSchedulerTasks.StopTask(_taskId);

        #endregion

        #region Private Members

        private async Task CheckNews()
        {
            var addedNewsItems = (await AddRecentNews()).ToList();
            if (addedNewsItems.Count == 0)
            {
                return;
            }

            await SendNews(addedNewsItems);
        }

        private async Task<IEnumerable<News>> AddRecentNews()
        {
            var currentLastNewsItems = (await _bCNewsList.GetNewsItems(Resources.NewsUrl)).ToList();
            if (currentLastNewsItems.Count == 0)
            {
                return null;
            }

            var addedNewsItems = new List<News>();

            var lastNewsItems = _bCNews.GetLastNewsItems().ToList();
            if (lastNewsItems.Count == 0)
            {
                Parallel.ForEach(currentLastNewsItems, x =>
                {
                    _bCNews.Create(x);
                    addedNewsItems.Add(x);
                });
            }
            else
            {
                foreach (var currentLastNewsItem in currentLastNewsItems)
                {
                    foreach (var lastNewsItem in lastNewsItems)
                    {
                        if (currentLastNewsItem.PublishDate == lastNewsItem.PublishDate &&
                            currentLastNewsItem.Url == lastNewsItem.Url &&
                            currentLastNewsItem.Title == lastNewsItem.Title)
                        {
                            continue;
                        }

                        _bCNews.Create(currentLastNewsItem);
                        addedNewsItems.Add(currentLastNewsItem);
                    }
                }
            }

            return addedNewsItems;
        }

        private async Task SendNews(IEnumerable<News> addedNewsItems)
        {
            var value = true.ToString().ToLower();
            var subscribedChatIds = _bcChatProperty.GetProperties(Resources.SubscriptionPropertyName, value)
                .Select(x => x.ChatId).ToList();

            foreach (var chatId in subscribedChatIds)
            {
                foreach (var newsItem in addedNewsItems)
                {
                    await SendNews(chatId, newsItem);
                }
            }
        }

        private async Task SendNews(long chatId, News item)
        {
            var message = _messageHandler.GetNewsMessage(item);
            var inlineMarkup = _inlineMarkupHandler.GetPostReplyMarkup(item.Url);
            var post = new PostDto(message, item.ImageUrl, inlineMarkup);

            await _botHandler.MakePost(_botClient, chatId, post);
        }

        #endregion
    }
}
