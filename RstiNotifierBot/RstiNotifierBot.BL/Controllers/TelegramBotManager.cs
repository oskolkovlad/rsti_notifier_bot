namespace RstiNotifierBot.BL.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using Telegram.Bot.Extensions.Polling;
    using Telegram.Bot.Types.Enums;
    using RstiNotifierBot.BL.Interfaces.Handlers;
    using RstiNotifierBot.Common.BL.Controllers;
    using RstiNotifierBot.Common.BL.Extensions;

    internal class TelegramBotManager : ITelegramBotManager
    {
        private const string StartBotMessage = "Bot \"@{0}\" has been started...";
        private const string StopBotMessage = "Bot \"@{0}\" finished work...";

        private static readonly object _lockObject;
        private static readonly CancellationTokenSource _cancellationTokenSource;

        private static ITelegramBotClient _client;

        private readonly ITelegramBotHandler _botHandler;

        public TelegramBotManager(ITelegramBotHandler botHandler)
        {
            _botHandler = botHandler;
        }

        static TelegramBotManager()
        {
            _lockObject = new object();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        #region Public Members

        public static ITelegramBotClient Client => GetClient();

        public async Task StartAsync()
        {
            var botInfo = await Client.GetMeAsync();

            //***//
            var message = string.Format(StartBotMessage, botInfo.Username);
            ConsoleHelper.OutputConsoleMessage(message, true, newLineAfter: true);
            //***//

            Client.StartReceiving(
                new DefaultUpdateHandler(
                    _botHandler.HandleUpdateAsync,
                    _botHandler.HandleErrorAsync,
                    new[] { UpdateType.Message }),
                _cancellationTokenSource.Token
            );
        }

        public async Task StopAsync()
        {
            if (Client != null)
            {
                var botInfo = await Client.GetMeAsync();

                //***//
                var message = string.Format(StopBotMessage, botInfo.Username);
                ConsoleHelper.OutputConsoleMessage(message, true, newLineAfter: true);
                //***//

                _cancellationTokenSource.Cancel();
            }
        }

        #endregion

        #region Private Members

        private static ITelegramBotClient GetClient()
        {
            if (_client == null)
            {
                lock (_lockObject)
                {
                    _client = new TelegramBotClient(Configuration.BotToken);
                }
            }

            return _client;
        }

        #endregion
    }
}
