namespace RstiNotifierBot
{
    using System;
    using System.Threading;
    using RstiNotifierBot.BusinessComponents;
    using RstiNotifierBot.Controllers.Commands;
    using RstiNotifierBot.Controllers.Handlers;
    using RstiNotifierBot.Controllers.Parsers;
    using RstiNotifierBot.Model.Repositories;

    internal class Program
    {
        private static TelegramBotManager _borProvider;

        private static void Main() => StartWorker();

        #region Private Members

        private async static void StartWorker()
        {
            try
            {
                InitializeDependencies();
                await _borProvider.Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
            Thread.Sleep(Timeout.Infinite);
        }

        private static void InitializeDependencies()
        {
            var newsParserController = new NewsParserController();
            var bcNewsList = new BCNewsList(newsParserController);
            var bcSchedulerTasks = new BCSchedulerTasks();
            
            var chatRepository = new ChatRepository();
            var chatPropertyRepository = new ChatPropertyRepository();
            var newsRepository = new NewsRepository();
            var bcChat = new BCChat(chatRepository);
            var bcChatProperty = new BCChatProperty(chatPropertyRepository);
            var bcNews = new BCNews(newsRepository);

            var messageHandler = new MessageHandler(bcNewsList);
            var subscriptionHandler = new SubscriptionHandler(bcChatProperty);

            var startCommand = new StartCommand(bcChat);
            var lastCommand = new LastCommand(messageHandler);
            var topCommand = new TopCommand(messageHandler);
            var subscribeCommand = new SubscribeCommand(subscriptionHandler);
            var unsubscribeCommand = new UnsubscribeCommand(subscriptionHandler);
            var infoCommand = new InfoCommand(messageHandler);
            var commandsInvoker = new CommandsInvoker(startCommand, lastCommand, topCommand,
                subscribeCommand, unsubscribeCommand, infoCommand);

            var botHandler = new TelegramBotHandler(commandsInvoker);
            _borProvider = new TelegramBotManager(botHandler);
        }

        #endregion
    }
}
