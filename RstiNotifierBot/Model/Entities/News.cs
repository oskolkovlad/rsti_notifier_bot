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

            //***//

            DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out var publishDate1);
            Console.WriteLine(publishDate1);
            Console.WriteLine();
            DateTime.TryParse(date, CultureInfo.CurrentCulture, DateTimeStyles.None, out var publishDate2);
            Console.WriteLine(publishDate2);
            Console.WriteLine();
            DateTime.TryParse(date, CultureInfo.DefaultThreadCurrentCulture, DateTimeStyles.None, out var publishDate3);
            Console.WriteLine(publishDate3);
            Console.WriteLine();
            DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var publishDate4);
            Console.WriteLine(publishDate4);
            Console.WriteLine();
            DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out var publishDate5);
            Console.WriteLine(publishDate5);
            Console.WriteLine();

            //***//

            DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out var publishDate);
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
