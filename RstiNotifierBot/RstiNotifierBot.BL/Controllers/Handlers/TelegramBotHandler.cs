namespace RstiNotifierBot.BL.Controllers.Handlers
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
    using RstiNotifierBot.BL.Dto;
    using RstiNotifierBot.BL.Dto.Commands;
    using RstiNotifierBot.BL.Interfaces.Commands;
    using RstiNotifierBot.BL.Interfaces.Extensions;
    using RstiNotifierBot.BL.Interfaces.Handlers;
    using RstiNotifierBot.Common.BL.Extensions;

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
            if (exception is ApiRequestException apiRequestException)
            {
                var message = $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}";
                Console.WriteLine(message);
            }
            else
            {
                exception.OutputConsoleLog();
            }

            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = BotOnMessageReceivedAsync(botClient, update.Message);
            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        public async Task MakePostAsync(ITelegramBotClient botClient, long chatId, PostDto post)
        {
            if (botClient == null || post == null)
            {
                return;
            }

            IReplyMarkup replyMarkup = null;

            var inlineMarkup = post.InlineMarkup;
            if (inlineMarkup != null && inlineMarkup.Any())
            {
                replyMarkup = CreateInlineReplyMarkup(inlineMarkup);
            }

            var message = post.Message;
            var imageUrl = post.ImageUrl;

            if (string.IsNullOrEmpty(imageUrl))
            {
                await botClient.SendTextMessageAsync(chatId, message, replyMarkup);
            }
            else
            {
                await botClient.SendImageMessageAsync(chatId, message, imageUrl, replyMarkup);
            }
        }

        #endregion

        #region Private Members

        private async Task BotOnMessageReceivedAsync(ITelegramBotClient botClient, Message message)
        {
            var messageChat = message.Chat;
            var chatId = messageChat.Id;
            var botInfo = await botClient.GetMeAsync();
            var command = message.Text.Clear($"@{botInfo.Username}");

            if (string.IsNullOrEmpty(command) || !command.StartsWith('/'))
            {
                return;
            }

            var chat = new RstiNotifierBot.Common.Model.Entities.Chat(chatId, messageChat.Username,
                messageChat.FirstName, messageChat.LastName, messageChat.Title, messageChat.Type.ToString());
            var context = new CommandContext(chat);
            var result = _commandsInvoker.Execute(context, command);
            if (!result.IsSuccess)
            {
                await TelegramBotExtensions.SendTextMessageAsync(botClient, chatId, ProcessingWarningMessage);
                return;
            }

            if (result is PostCommandResult postResult)
            {
                await MakePostAsync(botClient, chatId, postResult.Post);
            }
        }

        private static IReplyMarkup CreateInlineReplyMarkup(InlineButtonDto[][] inlineMarkup)
        {
            var lines = new List<List<InlineKeyboardButton>>();

            foreach (var buttonsLine in inlineMarkup)
            {
                var line = new List<InlineKeyboardButton>();

                foreach (var buttonInLine in buttonsLine)
                {
                    var button = new InlineKeyboardButton
                    {
                        Text = buttonInLine.Text,
                        Url = buttonInLine.Url,
                        CallbackData = buttonInLine.CallbackData
                    };

                    line.Add(button);
                }

                lines.Add(line);
            }

            return new InlineKeyboardMarkup(lines);
        }

        // TODO: не доделано.
        private static IReplyMarkup CreateReplyKeyboardMarkup(string[][] keyboardMarkup)
        {
            var lines = new List<List<KeyboardButton>>();

            foreach (var buttonsLine in keyboardMarkup)
            {
                var line = new List<KeyboardButton>();

                foreach (var buttonInLine in buttonsLine)
                {
                    var button = new KeyboardButton
                    {
                        Text = buttonInLine
                    };

                    line.Add(button);
                }

                lines.Add(line);
            }

            return new ReplyKeyboardMarkup(lines);
        }

        private static IReplyMarkup RemoveReplyKeyboardMarkup() => new ReplyKeyboardRemove();

        #endregion
    }
}
