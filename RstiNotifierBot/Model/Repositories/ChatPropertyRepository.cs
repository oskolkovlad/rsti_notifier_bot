namespace RstiNotifierBot.Model.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using Npgsql;
    using RstiNotifierBot.Interfaces.Model.Repositories;
    using RstiNotifierBot.Model.Entities;

    internal class ChatPropertyRepository : BaseRepository,
        IChatPropertyRepository
    {
        #region SQL Queries

        private const string InsertPropertyQuery =
            @"insert into chatproperty values(@ChatPropertyId, @ChatId, @Name, @Value)";

        private const string GetPropertiesQuery =
            @"select * from chatproperty
            where name = @name and value = @value";
        
        private const string GetPropertyQuery =
            @"select * from chatproperty
            where chatid = @chatId and name = @name and value = @value";

        private const string DeletePropertyQuery =
            @"delete from chatproperty where chatid = @chatId and name = @name";

        #endregion

        #region IChatPropertyRepository Members

        public void Create(ChatProperty item)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Execute(InsertPropertyQuery, item);
        }

        public IList<ChatProperty> GetProperties(string name, string value)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            return connection.Query<ChatProperty>(GetPropertiesQuery, new { name, value }).ToList();
        }

        public ChatProperty GetProperty(long chatId, string name, string value)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            return connection.Query<ChatProperty>(GetPropertyQuery, new { chatId, name, value }).FirstOrDefault();
        }

        public void Delete(long chatId, string name)
        {
            var connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Execute(DeletePropertyQuery, new { chatId, name });
        }

        #endregion
    }
}
