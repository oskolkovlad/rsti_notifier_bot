namespace RstiNotifierBot.Model
{
    internal class NewsHistory
    {
        public string Id { get; set; }

        public long ChatId { get; set; }

        public string NewsId { get; set; } // Внешний ключ.
    }
}
