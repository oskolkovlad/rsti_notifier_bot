namespace RstiNotifierBot
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using RstiNotifierBot.BusinessComponents;
    using RstiNotifierBot.Controllers.Commands;
    using RstiNotifierBot.Controllers.Handlers;
    using RstiNotifierBot.Controllers.Parsers;
    using RstiNotifierBot.Controllers.Services;
    using RstiNotifierBot.Extensions;
    using RstiNotifierBot.Interfaces.Controllers.Services;
    using RstiNotifierBot.Model.Repositories;

    internal class Program
    {
        private static TelegramBotManager _botManager;
        private static IService _newsTrackingService;

        private static async Task Main()
        {
            try
            {
                InitializeDependencies();
                await StartWorkAsync();
            }
            catch (Exception exception)
            {
                exception.OutputConsoleLog();
            }
            
            Thread.Sleep(Timeout.Infinite);
        }

        private static void InitializeDependencies()
        {
            var parserController = new NewsParserController();
            var bcNewsList = new BCNewsList(parserController);
            var bcSchedulerTasks = new BCSchedulerTasks();
            
            var chatRepository = new ChatRepository();
            var chatPropertyRepository = new ChatPropertyRepository();
            var newsRepository = new NewsRepository();
            var bcChat = new BCChat(chatRepository);
            var bcChatProperty = new BCChatProperty(chatPropertyRepository);
            var bcNews = new BCNews(newsRepository);

            var messageHandler = new MessageHandler(bcNewsList);
            var inlineMarkupHandler = new InlineMarkupHandler();
            var subscriptionHandler = new SubscriptionHandler(bcChatProperty);

            var startCommand = new StartCommand(bcChat);
            var lastCommand = new LastCommand(messageHandler, inlineMarkupHandler);
            var topCommand = new TopCommand(messageHandler);
            var subscribeCommand = new SubscribeCommand(subscriptionHandler);
            var unsubscribeCommand = new UnsubscribeCommand(subscriptionHandler);
            var infoCommand = new InfoCommand(messageHandler, inlineMarkupHandler);
            var commandsInvoker = new CommandsInvoker(startCommand, lastCommand, topCommand,
                subscribeCommand, unsubscribeCommand, infoCommand);

            var botHandler = new TelegramBotHandler(commandsInvoker);
            _botManager = new TelegramBotManager(botHandler);

            _newsTrackingService = new NewsTrackingService(TelegramBotManager.Client, botHandler,
                messageHandler, inlineMarkupHandler, bcNews, bcNewsList, bcChatProperty, bcSchedulerTasks);
        }

        private static async Task StartWorkAsync()
        {
            await _botManager.StartAsync();
            _newsTrackingService.Start();
        }
    }
}
