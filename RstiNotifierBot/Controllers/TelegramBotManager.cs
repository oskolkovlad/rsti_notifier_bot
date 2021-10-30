namespace RstiNotifierBot
{
    using System;
    using System.Threading;
    using Telegram.Bot;
    using Telegram.Bot.Extensions.Polling;
    using Telegram.Bot.Types.Enums;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;
    using System.Threading.Tasks;

    internal class TelegramBotManager
    {
        private const string StartBotMessage = "Bot \"@{0}\" has been started...";

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

        public async Task Start()
        {
            try
            {
                var botInfo = await Client.GetMeAsync();
                Console.WriteLine(StartBotMessage, botInfo.Username);

                Client.StartReceiving(
                    new DefaultUpdateHandler(
                        _botHandler.HandleUpdateAsync,
                        _botHandler.HandleErrorAsync,
                        new[] { UpdateType.Message }),
                    _cancellationTokenSource.Token
                );
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void Stop()
        {
            try
            {
                if (Client != null)
                {
                    _cancellationTokenSource.Cancel();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
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
