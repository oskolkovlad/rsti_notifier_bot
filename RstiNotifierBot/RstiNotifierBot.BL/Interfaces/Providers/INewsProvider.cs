namespace RstiNotifierBot.BL.Interfaces.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RstiNotifierBot.Common.Model.Entities;

    public interface INewsProvider
    {
        Task<News> GetLastNewsItemAsync(string url);

        Task<IEnumerable<News>> GetNewsItemsAsync(string url, bool reverse = true);
    }
}
