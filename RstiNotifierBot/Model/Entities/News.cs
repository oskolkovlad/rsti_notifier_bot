namespace RstiNotifierBot.Model.Entities
{
    using System;
    using System.Globalization;
    using System.Threading;

    internal class News
    {
        public News() { }

        public News(string title, string preview, string date, string url, string imageUrl)
        {
            Title = title;
            Preview = preview;
            Url = url;
            ImageUrl = imageUrl;

            Console.WriteLine(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
            Console.WriteLine(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern);

            var cultureInfo = cultureInfo = new CultureInfo("ru-RU");
            if (DateTime.TryParse(date, cultureInfo, DateTimeStyles.None, out var publishDate))
            {
                Console.WriteLine(cultureInfo.DateTimeFormat.ShortDatePattern);

                var publishDate1 = DateTime.ParseExact(date.Replace("&nbsp;", null), "dd/mm/yy", cultureInfo);
                Console.WriteLine("1 " + publishDate1.ToString("dd.mm.yy"));
                Console.WriteLine("1 " + publishDate1.ToString("dd.mm.yyyy"));

                var publishDate2 = DateTime.ParseExact(date.Replace("&nbsp;", null), "dd.mm.yy", cultureInfo);
                Console.WriteLine("2 " + publishDate2.ToString("dd.mm.yy"));
                Console.WriteLine("2 " + publishDate2.ToString("dd.mm.yyyy"));

                Console.WriteLine();

                publishDate = DateTime.ParseExact(date.Replace("&nbsp;", null), "dd.MM.yy", cultureInfo);
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
