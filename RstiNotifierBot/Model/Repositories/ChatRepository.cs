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
        #region SQL Queries

        private const string InsertChatQuery =
            @"insert into chat values (@ChatId, @Username, @FirstName, @LastName)";

        private const string GetChatsQuery =
            @"select* from chat";

        private const string GetChatByIdQuery =
            @"select * from chat where chatid = @chatId";

        private const string UpdateChatQuery =
            @"update chat 
            set username = @Username, firstname = @FirstName, lastname = @LastName
            where chatid = @ChatId)";

        private const string DeleteChatQuery =
            @"delete from chat where chatid = @chatId";

        #endregion

        #region IChatRepository Members

        public void Create(Chat chat)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Execute(InsertChatQuery, chat);
        }

        public IList<Chat> GetChats()
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            return connection.Query<Chat>(GetChatsQuery).ToList();
        }

        public Chat GetChatById(long chatId)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            return connection.Query<Chat>(GetChatByIdQuery, new { chatId }).FirstOrDefault();
        }

        public void Update(Chat chat)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Execute(UpdateChatQuery, chat);
        }

        public void Delete(long chatId)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Execute(DeleteChatQuery, new { chatId });
        }

        #endregion
    }
}
