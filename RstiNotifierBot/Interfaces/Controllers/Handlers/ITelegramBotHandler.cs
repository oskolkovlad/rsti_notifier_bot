namespace RstiNotifierBot.Interfaces.Controllers.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using Telegram.Bot.Types;
    using RstiNotifierBot.Dto;

    internal interface ITelegramBotHandler : IHandler
    {
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);
        
        Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

        Task MakePostAsync(ITelegramBotClient botClient, long chatId, PostDto post);
    }
}
