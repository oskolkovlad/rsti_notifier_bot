namespace RstiNotifierBot.CustomEventArgs
{
    using System;
    using RstiNotifierBot.Dto;

    internal class NewsEventArgs : EventArgs
    {

        public NewsEventArgs(long chatId, NewsDto newsItem)
        {
            ChatId = chatId;
            NewsItem = newsItem;
        }

        public long ChatId { get; private set; }

        public NewsDto NewsItem { get; private set; }
    }
}
