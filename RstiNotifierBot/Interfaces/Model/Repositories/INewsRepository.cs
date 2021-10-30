namespace RstiNotifierBot.Interfaces.Model.Repositories
{
    using System.Collections.Generic;
    using RstiNotifierBot.Model.Entities;

    internal interface INewsRepository
    {
        IList<News> GetNews();

        News GetNewsById(string newsId);

        void Create(News news);
    }
}
