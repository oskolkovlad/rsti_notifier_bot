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
                Console.WriteLine();

                Console.WriteLine(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);

                var cultureInfo = new CultureInfo("ru-US", true);
                Console.WriteLine(cultureInfo.DateTimeFormat.ShortDatePattern);

                var publishDate1 = DateTime.ParseExact(date.Replace("&nbsp;", null), "dd/mm/yy", cultureInfo);
                Console.WriteLine("1 " + publishDate1.ToString("dd.mm.yy"));
                Console.WriteLine("1 " + publishDate1.ToString("dd.mm.yyyy"));

                var publishDate2 = DateTime.ParseExact(date.Replace("&nbsp;", null), "dd.mm.yy", cultureInfo);
                Console.WriteLine("2 " + publishDate2.ToString("dd.mm.yy"));
                Console.WriteLine("2 " + publishDate2.ToString("dd.mm.yyyy"));

                var publishDate3 = DateTime.ParseExact(date, "dd/mm/yy", cultureInfo);
                Console.WriteLine("3 " + publishDate3.ToString("dd.mm.yy"));
                Console.WriteLine("3 " + publishDate3.ToString("dd.mm.yyyy"));

                var publishDate4 = DateTime.ParseExact(date, "dd.mm.yy", cultureInfo);
                Console.WriteLine("4 " + publishDate4.ToString("dd.mm.yy"));
                Console.WriteLine("4 " + publishDate4.ToString("dd.mm.yyyy"));

                publishDate = DateTime.ParseExact(date.Replace("&nbsp;", null), "dd.mm.yy", cultureInfo);
                
                Console.WriteLine();
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
