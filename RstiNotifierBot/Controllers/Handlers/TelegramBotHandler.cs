namespace RstiNotifierBot.Controllers.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using Telegram.Bot.Exceptions;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.ReplyMarkups;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Extensions;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;
    
    internal class TelegramBotHandler : ITelegramBotHandler
    {
        private const string ProcessingWarningMessage =
            "Данная команда не поддерживается ботом...Попробуте другую.";

        private readonly ICommandsInvoker _commandsInvoker;

        public TelegramBotHandler(ICommandsInvoker commandsInvoker)
        {
            _commandsInvoker = commandsInvoker;
        }

        #region ITelegramBotHandler Members

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                    $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(errorMessage);

            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = BotOnMessageReceived(botClient, update.Message);
            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        public static IReplyMarkup CreateInlineReplyMarkup(IEnumerable<IEnumerable<InlineButtonDto>> inlineMarkup)
        {
            var lines = new List<List<InlineKeyboardButton>>();

            var buttonsLines = inlineMarkup.ToList();
            foreach (var buttonsLine in buttonsLines)
            {
                var line = new List<InlineKeyboardButton>();

                var buttonsInLine = buttonsLine.ToList();
                foreach (var buttonInLine in buttonsInLine)
                {
                    var button = new InlineKeyboardButton
                    {
                        Text = buttonInLine.Text,
                        Url = buttonInLine.Url,
                        CallbackData = buttonInLine.CallbackData,
                    };

                    line.Add(button);
                }

                lines.Add(line);
            }

            return new InlineKeyboardMarkup(lines);
        }

        #endregion

        #region Private Members

        private async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            var chatId = message.Chat.Id;
            var botInfo = await botClient.GetMeAsync();
            var command = message.Text.Clear($"@{botInfo.Username}");

            if (string.IsNullOrEmpty(command) || !command.StartsWith('/'))
            {
                return;
            }
            
            var chat = new CommandContext(message.Chat);
            var result = await _commandsInvoker.Execute(chat, command);
            if (!result.IsSuccess)
            {
                await botClient.SendTextMessage(chatId, ProcessingWarningMessage);
                return;
            }

            if (result is PostCommandResult postResult)
            {
                await MakePost(botClient, chatId, postResult);
            }
        }

        private async Task MakePost(ITelegramBotClient botClient, long chatId, PostCommandResult result)
        {
            IReplyMarkup replyMarkup = null;

            var inlineMarkup = result.InlineMarkup;
            if (inlineMarkup != null && inlineMarkup.Any())
            {
                replyMarkup = CreateInlineReplyMarkup(inlineMarkup);
            }

            var message = result.Message;
            var imageUrl = result.ImageUrl;

            if (string.IsNullOrEmpty(imageUrl))
            {
                await botClient.SendTextMessage(chatId, message, replyMarkup);
            }
            else
            {
                await botClient.SendImageMessage(chatId, message, imageUrl, replyMarkup);
            }
        }

        #endregion
    }
}
