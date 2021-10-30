namespace RstiNotifierBot.Interfaces.Model.Repositories
{
    using System.Collections.Generic;
    using RstiNotifierBot.Model.Entities;

    internal interface INewsRepository
    {
        void Create(News news);

        IList<News> GetNews();

        News GetNewsById(string newsId);
    }
}
