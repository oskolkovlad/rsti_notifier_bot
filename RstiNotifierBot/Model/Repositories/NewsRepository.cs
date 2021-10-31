namespace RstiNotifierBot.Model.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using RstiNotifierBot.Interfaces.Model.Repositories;
    using RstiNotifierBot.Model.Entities;

    internal class NewsRepository : BaseRepository,
        INewsRepository
    {
        #region SQL Queries

        private const string InsertNewsQuery =
            @"insert into news(title, preview, url, imageUrl, publishDate)
            values(@Title, @Preview, @Url, @ImageUrl, @PublishDate)";

        private const string GetNewsQuery =
            @"select * from news";

        private readonly string GetLastNewsQuery = "{0} order by newsid desc limit {1}";

        private const string GetNewsByIdQuery =
            @"select * from news where newsid = @newsId";

        #endregion

        #region INewsRepository Members

        public void Create(News item) => ExecuteQuery(InsertNewsQuery, item);

        public IList<News> GetNews() => GetQueryResult<News>(GetNewsQuery);

        public IList<News> GetLastNews(int count = 15) =>
            GetQueryResult<News>(string.Format(GetLastNewsQuery, GetNewsQuery, count));

        public News GetNewsById(string newsId) =>
            GetQueryResult<News>(GetNewsByIdQuery, new { newsId }).FirstOrDefault();

        #endregion
    }
}
