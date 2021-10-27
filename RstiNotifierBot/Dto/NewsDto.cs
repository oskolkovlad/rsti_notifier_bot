namespace RstiNotifierBot.Dto
{
    using System;

    internal class NewsDto
    {
        public NewsDto(string title, string preview, string date, string url, string imageUrl)
        {
            Title = title;
            Preview = preview;
            Url = url;
            ImageUrl = imageUrl;

            DateTime.TryParse(date, out var dateTime);
            Date = dateTime;
        }

        public string Title { get; private set; }

        public string Preview { get; private set; }

        public DateTime Date { get; private set; }

        public string Url { get; private set; }

        public string ImageUrl { get; private set; }
    }
}
