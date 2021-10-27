namespace RstiNotifierBot.Dto
{
    internal class NewsDto
    {
        public NewsDto(string title, string preview, string date, string url, string imageUrl)
        {
            Title = title;
            Preview = preview;
            Date = date;
            Url = url;
            ImageUrl = imageUrl;
        }

        public string Title { get; private set; }

        public string Preview { get; private set; }

        public string Date { get; private set; }

        public string Url { get; private set; }

        public string ImageUrl { get; private set; }
    }
}
