namespace RstiNotifierBot.Interfaces.Model.Repositories
{
    using System.Collections.Generic;
    using RstiNotifierBot.Model.Entities;

    internal interface INewsRepository
    {
        void Create(News item);

        IList<News> GetNews();

        IList<News> GetLastNews(int count = 15);

        News GetNewsById(string newsId);
    }
}
