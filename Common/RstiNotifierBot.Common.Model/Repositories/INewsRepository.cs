namespace RstiNotifierBot.Common.Model.Repositories
{
    using System.Collections.Generic;
    using RstiNotifierBot.Common.Model.Entities;

    public interface INewsRepository : IRepository
    {
        void Create(News item);

        IList<News> GetNews();

        IList<News> GetLastNews(int count = 15);

        News GetNewsById(string newsId);
    }
}
