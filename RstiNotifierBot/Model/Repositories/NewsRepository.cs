namespace RstiNotifierBot.Model.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using Npgsql;
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

        public void Create(News item)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Execute(InsertNewsQuery, item);
        }

        public IList<News> GetNews()
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            return connection.Query<News>(GetNewsQuery).ToList();
        }

        public IList<News> GetLastNews(int count = 15)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            var sqlQuery = string.Format(GetLastNewsQuery, GetNewsQuery, count);
            return connection.Query<News>(sqlQuery).ToList();
        }

        public News GetNewsById(string newsId)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            return connection.Query<News>(GetNewsByIdQuery, new { newsId }).FirstOrDefault();
        }

        #endregion
    }
}
