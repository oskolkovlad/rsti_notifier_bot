namespace RstiNotifierBot.Model.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using Npgsql;
    using RstiNotifierBot.Interfaces.Model.Repositories;
    using RstiNotifierBot.Model.Entities;

    internal class ChatRepository : BaseRepository,
        IChatRepository
    {
        #region IChatRepository Members

        public IList<Chat> GetChats()
        {
            var connectionString = GetConnectionString();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                return connection.Query<Chat>("select * from chat").ToList();
            }
        }

        public Chat GetChatById(long chatId)
        {
            var connectionString = GetConnectionString();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                return connection.Query<Chat>( "select * from chat where chatid = @ChatId", new { chatId })
                    .FirstOrDefault();
            }
        }

        public void Create(Chat chat)
        {
            var connectionString = GetConnectionString();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = "insert into chat values(@ChatId, @Username, @FirstName, @LastName)";
                connection.Execute(sqlQuery, chat);
            }
        }

        public void Update(Chat chat)
        {
            var connectionString = GetConnectionString();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = "update chat set username = @Username, firstname = @FirstName, lastname = @LastName where chatid = @ChatId)";
                connection.Execute(sqlQuery, chat);
            }
        }

        public void Delete(long chatId)
        {
            var connectionString = GetConnectionString();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = "delete from chat where chatid = @chatId";
                connection.Execute(sqlQuery, new { chatId });
            }
        }

        #endregion
    }
}
