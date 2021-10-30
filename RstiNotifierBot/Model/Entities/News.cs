namespace RstiNotifierBot.Model.Entities
{
    using System;

    internal class News
    {
        public string NewsId { get; set; }

        public string Title { get; set; }

        public string Preview { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
