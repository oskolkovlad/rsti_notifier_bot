namespace RstiNotifierBot.Model.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using Npgsql;

    internal class BaseRepository
    {
        private const string DatabaseUrlVariable = "DATABASE_URL";

        #region Protected Members

        protected IList<TItem> GetQueryResult<TItem>(string sqlQuery, object param = null)
        {
            try
            {
                var connectionString = GetConnectionString();
                using var connection = new NpgsqlConnection(connectionString);
                return connection.Query<TItem>(sqlQuery, param).ToList();
            }
            catch
            {
                throw;
            }
        }

        protected void ExecuteQuery(string sqlQuery, object param = null)
        {
            try
            {
                var connectionString = GetConnectionString();
                using var connection = new NpgsqlConnection(connectionString);
                connection.Execute(sqlQuery, param);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Private Members

        private static string GetConnectionString()
        {
#if DEBUG
            return GetDefaultConnectionString();
#endif
            return GetHerokuConnectionString();
        }

        private static string GetDefaultConnectionString()
        {
            return null;
        }

        private static string GetHerokuConnectionString()
        {
            var databaseUrl = Environment.GetEnvironmentVariable(DatabaseUrlVariable);
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                Pooling = true,
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            return builder.ToString();
        }

        #endregion
    }
}
