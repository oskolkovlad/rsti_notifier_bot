namespace RstiNotifierBot.Model.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Dapper;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Npgsql;
    using RstiNotifierBot.Model.Constants;

    internal class BaseRepository
    {
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
            string connectionString = null;

            using (var stream = File.OpenText(Credentials.CredentialJsonFileName))
            using (var reader = new JsonTextReader(stream))
            {
                var credentials = JObject.Load(reader);
                var properties = credentials
                    .SelectToken(Credentials.ConnectionStringToken)
                    .SelectToken(Credentials.DefaultConnectionStringToken)
                    .Children()
                    .Cast<JProperty>()
                    .Select(x => $"{x.Name}={x.Value}");

                connectionString = string.Join(";", properties);
            }

            return connectionString;
        }

        private static string GetHerokuConnectionString()
        {
            var databaseUrl = Environment.GetEnvironmentVariable(Credentials.DatabaseUrlVariable);
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
