namespace RstiNotifierBot.Common.Model.Entities
{
    using System;
    using System.Globalization;

    public class News
    {
        public News() { }

        public News(string title, string preview, string date, string url, string imageUrl)
        {
            Title = title;
            Preview = preview;
            Url = url;
            ImageUrl = imageUrl;

            ConvertStringToDate(date);
        }

        #region Public Members

        public string NewsId { get; set; }

        public string Title { get; set; }

        public string Preview { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }

        #endregion

        #region Private Members

        private void ConvertStringToDate(string date)
        {
            var cultureInfo = new CultureInfo("ru-RU");
            if (DateTime.TryParse(date, cultureInfo, DateTimeStyles.None, out var publishDate))
            {
                publishDate = DateTime.ParseExact(date.Replace("&nbsp;", null), "dd.MM.yy", cultureInfo);
            }
            PublishDate = publishDate;
        }

        #endregion
    }
}
