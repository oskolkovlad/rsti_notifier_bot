namespace RstiNotifierBot.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Telegram.Bot;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using Telegram.Bot.Types.ReplyMarkups;
    using RstiNotifierBot.Interfaces.Controllers;
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Properties;
    using Telegram.Bot.Types.InputFiles;
    using RstiNotifierBot.Extensions;

    internal class TelegramBotProvider
    {
        private const string AllNewsCaption = "Все новости";
        private const string InstagramCaption = "Инстаграм";
        private const string LinkCaption = "Перейти";
        private const string ProcessingWarningMessage = "Данный тип обновления не обрабатывается.";

        private readonly TelegramBotClient client;
        private readonly IComandsProvider commandsProvider;
        private readonly string name;

        public TelegramBotProvider(string token, IComandsProvider commandsProvider)
        {
            this.commandsProvider = commandsProvider;
            commandsProvider.NotifyUser += (sender, args) => ProcessingMessageUpdate(args.ChatId, Commands.Last); // TODO

            client = new TelegramBotClient(token);

            var info = client.GetMeAsync().Result;
            name = $"@{info.Username}";
        }

        public async void ListenUpdates()
        {
            await client.SetWebhookAsync(string.Empty);

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

        private async void ProcessingUpdate(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    var message = update.Message;
                    ProcessingMessageUpdate(message.Chat.Id, message.Text);
                    break;

                case UpdateType.CallbackQuery:
                    ProcessingCallbackQueryUpdate(update.CallbackQuery);
                    break;

                default:
                    Console.WriteLine(ProcessingWarningMessage);
                    return;
            }
        }

        private async void ProcessingMessageUpdate(long chatId, string message)
        {
            var command = message.Clear(name);
  
            var answerResult = commandsProvider.GetAnswer(command, chatId);
            if (answerResult == null || !answerResult.IsSuccess)
            {
                return;
            }

            if (command == Commands.Subscribe || command == Commands.Unsubscribe)
            {
                return;
            }
            
            var answer = answerResult.Answer;
            var replyMarkup = GetReplyMarkup(answerResult);

            try
            {
                var imageUrl = answerResult.NewsItem?.ImageUrl;
                if (command == Commands.Last && !string.IsNullOrEmpty(imageUrl))
                {
                    var imageFile = new InputOnlineFile(answerResult.NewsItem.ImageUrl);
                    await client.SendPhotoAsync(chatId, imageFile, answer, ParseMode.Default, null, false, 0,
                        false, replyMarkup);
                }
                else
                {
                    await client.SendTextMessageAsync(chatId, answer, ParseMode.Default, null, false, false, 0,
                        false, replyMarkup);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }     
        }

        private async void ProcessingCallbackQueryUpdate(CallbackQuery callbackQuery) { }

        #region Reply Markup

        private IReplyMarkup GetReplyMarkup(AnswerResult answerResult)
        {
            IReplyMarkup replyMarkup;
            switch (answerResult.Command)
            {
                case Commands.Last:
                case Commands.Top5:
                    replyMarkup = GetLinkInlineReplyMarkup(answerResult.NewsItem?.Url);
                    break;

                case Commands.Info:
                    replyMarkup = GetLinkInlineReplyMarkup();
                    break;

                default:
                    replyMarkup = null;
                    break;
            }

            return replyMarkup;
        }

        private static IReplyMarkup GetLinkInlineReplyMarkup(string url)
        {
            return !string.IsNullOrEmpty(url)
                ? new InlineKeyboardMarkup(new InlineKeyboardButton { Text = LinkCaption, Url = url })
                : null;
        }

        private static IReplyMarkup GetLinkInlineReplyMarkup()
        {
            return new InlineKeyboardMarkup(
                new List<InlineKeyboardButton> {
                    new InlineKeyboardButton { Text = AllNewsCaption, Url = Resources.NewsUrl },
                    new InlineKeyboardButton { Text = InstagramCaption, Url = Resources.InstagramUrl }
                });
        }

        #endregion

        #endregion
    }
}
