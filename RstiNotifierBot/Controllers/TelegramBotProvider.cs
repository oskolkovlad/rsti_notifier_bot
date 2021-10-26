namespace RstiNotifierBot.Controllers
{
    using System;
    using System.Threading;
    using Telegram.Bot;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using RstiNotifierBot.Interfaces.Controllers;

    internal class TelegramBotProvider
    {
        private readonly TelegramBotClient client;
        private readonly IAnswerProvider answerProvider;

        public TelegramBotProvider(string token, IAnswerProvider answerProvider)
        {
            this.answerProvider = answerProvider;

            client = new TelegramBotClient(token);
        }

        public async void ListenUpdates()
        {
            var offset = 0;

            while (true)
            {
                try
                {
                    var updates = await client.GetUpdatesAsync(offset);
                    if (updates != null && updates.Length > 0)
                    {
                        foreach (var update in updates)
                        {
                            ProcessingUpdate(update);
                            offset = update.Id + 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(1000);
            }
        }

        #region Private Members

        private void ProcessingUpdate(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    var message = update.Message;
                    var chatId = message.Chat.Id;
                    var answer = answerProvider.GetAnswer(message.Text);
                    client.SendTextMessageAsync(chatId, answer);
                    break;

                default:
                    Console.WriteLine("данный тип обновления не обрабатывается.");
                    return;
            }
        }

        #endregion
    }
}
