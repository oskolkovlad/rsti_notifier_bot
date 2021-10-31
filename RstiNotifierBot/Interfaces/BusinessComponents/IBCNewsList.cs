namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RstiNotifierBot.Model.Entities;

    internal interface IBCNewsList : IBCComponent
    {
        Task<News> GetLastNewsItem(string url);

        Task<IEnumerable<News>> GetNewsItems(string url);
    }
}
