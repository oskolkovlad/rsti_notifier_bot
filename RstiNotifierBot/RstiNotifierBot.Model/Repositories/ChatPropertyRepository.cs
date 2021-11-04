namespace RstiNotifierBot.Model.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using RstiNotifierBot.Common.Model.Entities;
    using RstiNotifierBot.Common.Model.Repositories;

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

        public void Create(ChatProperty item) => ExecuteQuery(InsertPropertyQuery, item);
        
        public IList<ChatProperty> GetProperties(string name, string value) =>
            GetQueryResult<ChatProperty>(GetPropertiesQuery, new { name, value });

        public ChatProperty GetProperty(long chatId, string name, string value) =>
            GetQueryResult<ChatProperty>(GetPropertyQuery, new { chatId, name, value }).FirstOrDefault();

        public void Delete(long chatId, string name) => ExecuteQuery(DeletePropertyQuery, new { chatId, name });

        #endregion
    }
}
