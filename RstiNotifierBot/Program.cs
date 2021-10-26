namespace RstiNotifierBot
{
    using System;
    using RstiNotifierBot.Controllers;
    using RstiNotifierBot.Controllers.Parsers;

    // TODO: DI.
    // TODO: Buttons.
    // TODO: beautify post.
    // TODO: add commands (Commands pattern).
    // TODO: timer notify.

    internal class Program
    {
        private const string Token = "2079782412:AAG081yNVkR3OC6bI5DOj2iA3YqTkTPWv0c";

        #region Private Members

        private static void Main()
        {
            var parserController = new NewsParserController();
            var answerProvider = new AnswerProvider(parserController);
            var botProvider = new TelegramBotProvider(Token, answerProvider);

            botProvider.ListenUpdates();

            Console.ReadKey();
        }

        #endregion
    }
}
