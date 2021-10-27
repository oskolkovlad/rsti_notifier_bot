namespace RstiNotifierBot
{
    using System;
    using RstiNotifierBot.BusinessComponents;
    using RstiNotifierBot.Controllers;
    using RstiNotifierBot.Controllers.Parsers;

    // TODO: DI.
    // TODO: beautify post.
    // TODO: add commands (Commands pattern).

    internal class Program
    {
        private const string Token = "2079782412:AAG081yNVkR3OC6bI5DOj2iA3YqTkTPWv0c";

        #region Private Members

        private static void Main()
        {
            var parserController = new NewsParserController();
            var bcSchedulerTasks = new BCSchedulerTasks();
            var commandsProvider = new ComandsProvider(parserController, bcSchedulerTasks);
            var botProvider = new TelegramBotProvider(Token, commandsProvider);

            botProvider.ListenUpdates();

            Console.ReadKey();
        }

        #endregion
    }
}
