namespace RstiNotifierBot.Model.Repositories
{
    using System;
    using Npgsql;

    internal class BaseRepository
    {
        #region Protected Members

        protected string GetConnectionString()
        {
#if DEBUG
            return Configuration.DefaultConectionString;
#endif

            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };

            return builder.ToString();
        }

        #endregion
    }
}
