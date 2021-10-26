namespace RstiNotifierBot.BusinessObjects.Constants
{
    internal static class NewsElements
    {
        public const string News = ".//div[@class='news-list-item span4']";
        public const string Title = ".//a[@class='news-list-item__title']";
        public const string Preview = ".//div[@class='news-list-item__preview']";
        public const string Date = ".//span[@class='news-list-item__date']";
    }
}
