namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RstiNotifierBot.Model.Entities;

    internal interface IBCNewsList : IBCComponent
    {
        Task<News> GetLastNewsItemAsync(string url);

        Task<IEnumerable<News>> GetNewsItemsAsync(string url, bool reverse = true);
    }
}
