namespace RstiNotifierBot.Model
{
    using System;

    internal class News
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Preview { get; set; }

        public string Url { get; set; }

        public DateTime Date { get; set; }
    }
}
