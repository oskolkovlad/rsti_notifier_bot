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
        #region INewsRepository Members

        public IList<News> GetNews()
        {
            var connectionString = GetConnectionString();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                return connection.Query<News>("select * from news").ToList();
            }
        }

        public News GetNewsById(string newsId)
        {
            var connectionString = GetConnectionString();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                return connection.Query<News>( "select * from chat where newsid = @newsId", new { newsId })
                    .FirstOrDefault();
            }
        }

        public void Create(News news)
        {
            var connectionString = GetConnectionString();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = "insert into news values(@NewsId, @Title, @Preview, @Url, @ImageUrl, @PublishDate)";
                connection.Execute(sqlQuery, news);
            }
        }

        #endregion
    }
}
