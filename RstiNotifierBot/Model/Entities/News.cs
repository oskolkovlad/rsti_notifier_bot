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
                Console.WriteLine(CultureInfo.CurrentCulture);

                var cultureInfo = new CultureInfo("ru-RU", false);
                publishDate = DateTime.ParseExact(date.Replace("&nbsp;", null), "dd.mm.yy", cultureInfo);
                
                DateTime.TryParse(date.Replace("&nbsp;", null), cultureInfo, DateTimeStyles.None, out var ppp);
                Console.WriteLine(ppp.ToString("dd.mm.yy"));
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
