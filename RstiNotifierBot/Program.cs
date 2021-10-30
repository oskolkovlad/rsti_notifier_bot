﻿namespace RstiNotifierBot
{
    using System;
    using System.Threading;
    using RstiNotifierBot.BusinessComponents;
    using RstiNotifierBot.Controllers.Commands;
    using RstiNotifierBot.Controllers.Handlers;
    using RstiNotifierBot.Controllers.Parsers;

    // TODO: DI.
    // TODO: beautify post.

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
            var bcChatProperty = new BCChatProperty();
            var bcNewsProperty = new BCNewsProperty();

            var messageHandler = new MessageHandler(bcNewsList);
            var subscribtionHandler = new SubscribtionHandler(bcChatProperty);

            var lastCommand = new LastCommand(messageHandler);
            var topCommand = new TopCommand(messageHandler);
            var subscribeCommand = new SubscribeCommand(subscribtionHandler);
            var unsubscribeCommand = new UnsubscribeCommand(subscribtionHandler);
            var infoCommand = new InfoCommand(messageHandler);
            var commandsInvoker = new CommandsInvoker(lastCommand, topCommand, subscribeCommand,
                unsubscribeCommand, infoCommand);

            var botHandler = new TelegramBotHandler(commandsInvoker);
            _borProvider = new TelegramBotManager(botHandler);
        }

        #endregion
    }
}
