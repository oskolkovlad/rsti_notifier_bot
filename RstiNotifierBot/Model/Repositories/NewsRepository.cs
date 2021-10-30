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
            @"insert into news values (@NewsId, @Title, @Preview, @Url, @ImageUrl, @PublishDate)";

        private const string GetNewsQuery =
            @"select * from news";

        private const string GetNewsByIdQuery =
            @"select * from news where newsid = @newsId";

        #endregion

        #region INewsRepository Members

        public void Create(News news)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Execute(InsertNewsQuery, news);
        }

        public IList<News> GetNews()
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            return connection.Query<News>(GetNewsQuery).ToList();
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
