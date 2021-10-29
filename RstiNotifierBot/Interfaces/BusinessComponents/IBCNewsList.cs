namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RstiNotifierBot.Dto;

    internal interface IBCNewsList
    {
        Task<NewsDto> GetLastNewsItem(string url);

        Task<IEnumerable<NewsDto>> GetNewsItems(string url);
    }
}
