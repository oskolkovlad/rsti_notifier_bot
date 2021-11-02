namespace RstiNotifierBot.BL.Builders
{
    using RstiNotifierBot.BL.Controllers;
    using RstiNotifierBot.BL.Controllers.Commands;
    using RstiNotifierBot.BL.Controllers.Handlers;
    using RstiNotifierBot.BL.Controllers.Parsers;
    using RstiNotifierBot.BL.Controllers.Providers;
    using RstiNotifierBot.BL.Services;
    using RstiNotifierBot.Common.BC;
    using RstiNotifierBot.Common.BL.Controllers;
    using RstiNotifierBot.Common.BL.Services;
    using RstiNotifierBot.Infrastructure;
    using RstiNotifierBot.Infrastructure.BC;
    using RstiNotifierBot.Infrastructure.BO.Dto;

    public class BLBuilder : BaseBuilder
    {
        private const string BotClientParameterName = "botClient";
        private const string BotHandlerParameterName = "botHandler";
        private const string MessageHandlerParameterName = "messageHandler";
        private const string InlineMarkupHandlerParameterName = "inlineMarkupHandler";
        private const string NewsProviderParameterName = "newsProvider";
        private const string BCNewsParameterName = "bcNews";
        private const string BCChatPropertyParameterName = "bcChatProperty";
        private const string BCSchedulerTasksParameterName = "bcSchedulerTasks";

        #region BaseBuilder Members

        public override void Build(ComponentsContainer container)
        {  
            // Business Components.
            var bcChat = BCContainer.Instance.Get<IBCChat>();
            var bcChatProperty = BCContainer.Instance.Get<IBCChatProperty>();
            var bcNews = BCContainer.Instance.Get<IBCNews>();
            var bcSchedulerTasks = BCContainer.Instance.Get<IBCSchedulerTasks>();

            // Providers.
            var parserController = new NewsParserController();
            var newsProvider = new NewsProvider(parserController);

            // Handlers.
            var messageHandler = new MessageHandler(newsProvider);
            var inlineMarkupHandler = new InlineMarkupHandler();
            var subscriptionHandler = new SubscriptionHandler(bcChatProperty);

            // Commands.
            var startCommand = new StartCommand(bcChat);
            var lastCommand = new LastCommand(messageHandler, inlineMarkupHandler);
            var topCommand = new TopCommand(messageHandler);
            var subscribeCommand = new SubscribeCommand(subscriptionHandler);
            var unsubscribeCommand = new UnsubscribeCommand(subscriptionHandler);
            var infoCommand = new InfoCommand(messageHandler, inlineMarkupHandler);
            var commandsInvoker = new CommandsInvoker(startCommand, lastCommand, topCommand,
                subscribeCommand, unsubscribeCommand, infoCommand);

            var botHandler = new TelegramBotHandler(commandsInvoker);

            // Register main BL Components.
            var botHandlerParameter = new RegisterParameter(BotHandlerParameterName, botHandler);
            container.Register<TelegramBotManager, ITelegramBotManager>(true, botHandlerParameter);

            var parameters = new[]
            {
                new RegisterParameter(BotClientParameterName, TelegramBotManager.Client),
                botHandlerParameter,
                new RegisterParameter(MessageHandlerParameterName, messageHandler),
                new RegisterParameter(InlineMarkupHandlerParameterName, inlineMarkupHandler),
                new RegisterParameter(NewsProviderParameterName, newsProvider),
                new RegisterParameter(BCNewsParameterName, bcNews),
                new RegisterParameter(BCChatPropertyParameterName, bcChatProperty),
                new RegisterParameter(BCSchedulerTasksParameterName, bcSchedulerTasks),
            };
            container.Register<NewsTrackingService, INewsTrackingService>(true, parameters);
        }
        
        #endregion
    }
}
