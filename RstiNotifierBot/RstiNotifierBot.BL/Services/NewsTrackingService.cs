namespace RstiNotifierBot.BL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using RstiNotifierBot.BL.Dto;
    using RstiNotifierBot.BL.Interfaces.Handlers;
    using RstiNotifierBot.BL.Interfaces.Providers;
    using RstiNotifierBot.BL.Properties;
    using RstiNotifierBot.Common.BC;
    using RstiNotifierBot.Common.BL.Extensions;
    using RstiNotifierBot.Common.Model.Entities;
    using RstiNotifierBot.Common.BL.Services;

    internal class NewsTrackingService : BaseService,
        INewsTrackingService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ITelegramBotHandler _botHandler;
        private readonly IMessageHandler _messageHandler;
        private readonly IInlineMarkupHandler _inlineMarkupHandler;
        private readonly INewsProvider _newsProvider;
        private readonly IBCNews _bcNews;
        private readonly IBCChatProperty _bcChatProperty;
        private readonly IBCSchedulerTasks _bcSchedulerTasks;

        private readonly string _taskId;

        public NewsTrackingService(
            ITelegramBotClient botClient,
            ITelegramBotHandler botHandler,
            IMessageHandler messageHandler,
            IInlineMarkupHandler inlineMarkupHandler,
            INewsProvider newsProvider,
            IBCNews bcNews,
            IBCChatProperty bcChatProperty,
            IBCSchedulerTasks bcSchedulerTasks)
        {
            _botClient = botClient;
            _botHandler = botHandler;
            _messageHandler = messageHandler;
            _inlineMarkupHandler = inlineMarkupHandler;
            _newsProvider = newsProvider;
            _bcNews = bcNews;
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

        private const string CheckNewsStartMessage = "Start checking news for relevance... (NewsTrackingService)";
        private const string CheckNewsStopMessage = "Stop checking news for relevance... (NewsTrackingService)";
        private const string AddingNewsStartMessage = "Start adding new news...";
        private const string SendingNewsStartMessage = "Start sending new news...";

        private async Task CheckNewsAsync()
        {
            //***//
            ConsoleHelper.OutputNowDateTime(withFinishSeparator: false, newLineAfter: true);
            ConsoleHelper.OutputConsoleMessage(CheckNewsStartMessage, false, false, false);
            //***//

            try
            {
                var addedNewsItems = (await AddRecentNewsAsync()).ToList();
                if (addedNewsItems.Count != 0)
                {
                    await SendNewsAsync(addedNewsItems);
                }

                //***//
                ConsoleHelper.OutputConsoleMessage(CheckNewsStopMessage, withStartSeparator: false);
                //***//
            }
            catch (Exception exception)
            {
                exception.OutputConsoleLog();
            }
        }

        private async Task<IEnumerable<News>> AddRecentNewsAsync()
        {
            //***//
            ConsoleHelper.OutputConsoleMessage(AddingNewsStartMessage, false, false, false);
            //***//

            var currentLastNewsItems = (await _newsProvider.GetNewsItemsAsync(Resources.NewsUrl)).ToList();
            if (currentLastNewsItems.Count == 0)
            {
                return currentLastNewsItems;
            }

            var addedNewsItems = new List<News>();

            var lastNewsItems = _bcNews.GetLastNewsItems().ToList();
            if (lastNewsItems.Count == 0)
            {
                foreach (var currentLastNewsItem in currentLastNewsItems)
                {
                    _bcNews.Create(currentLastNewsItem);
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
                        _bcNews.Create(currentLastNewsItem);
                        addedNewsItems.Add(currentLastNewsItem);
                    }
                }
            }

            //***//
            ConsoleHelper.OutputConsoleMessage($"{addedNewsItems.Count} news were added.", false, false, false);
            //***//

            return addedNewsItems;
        }

        private async Task SendNewsAsync(IEnumerable<News> addedNewsItems)
        {
            //***//
            ConsoleHelper.OutputConsoleMessage(SendingNewsStartMessage, false, false, false);
            //***//

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
            //***//
            ConsoleHelper.OutputConsoleMessage($"Send news to chat ({chatId})...", false, false, false);
            //***//

            var message = _messageHandler.GetNewsMessage(item);
            var inlineMarkup = _inlineMarkupHandler.GetPostReplyMarkup(item.Url);
            var post = new PostDto(message, item.ImageUrl, inlineMarkup);

            await _botHandler.MakePostAsync(_botClient, chatId, post);
        }

        #endregion
    }
}
