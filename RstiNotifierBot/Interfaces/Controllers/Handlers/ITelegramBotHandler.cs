namespace RstiNotifierBot.Interfaces.Controllers.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using Telegram.Bot.Types;

    internal interface ITelegramBotHandler
    {
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);
        
        Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
