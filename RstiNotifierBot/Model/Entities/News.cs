namespace RstiNotifierBot.Model.Entities
{
    using System;
    using System.Globalization;

    internal class News
    {
        public News() { }

        public News(string title, string preview, string date, string url, string imageUrl)
        {
            Title = title;
            Preview = preview;
            Url = url;
            ImageUrl = imageUrl;

            if (DateTime.TryParse(date, out var publishDate))
            {
                publishDate = DateTime.ParseExact(date.Replace("&nbsp;", null), "dd/mm/yyyy", CultureInfo.InvariantCulture);
            }
            PublishDate = publishDate;
        }

        public string NewsId { get; set; }

        public string Title { get; set; }

        public string Preview { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
