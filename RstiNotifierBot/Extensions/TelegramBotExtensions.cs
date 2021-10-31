namespace RstiNotifierBot.Extensions
{
    using System.Threading.Tasks;
    using Telegram.Bot;
    using Telegram.Bot.Types.Enums;
    using Telegram.Bot.Types.InputFiles;
    using Telegram.Bot.Types.ReplyMarkups;

    internal static class TelegramBotExtensions
    {
        public static async Task SendTextMessageAsync(this ITelegramBotClient botClient, long chatId, string message,
            IReplyMarkup replyMarkup = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            await botClient.SendTextMessageAsync(chatId, message, ParseMode.Markdown, replyMarkup: replyMarkup);
        }

        public static async Task SendImageMessageAsync(this ITelegramBotClient botClient, long chatId, string message,
            string imageUrl, IReplyMarkup replyMarkup = null)
        {
            if (string.IsNullOrEmpty(imageUrl) || string.IsNullOrEmpty(message))
            {
                return;
            }

            var imageFile = new InputOnlineFile(imageUrl);
            await botClient.SendPhotoAsync(chatId, imageFile, message, ParseMode.Markdown, replyMarkup: replyMarkup);
        }
    }
}
