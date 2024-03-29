﻿namespace RstiNotifierBot.Model.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using RstiNotifierBot.Common.Model.Entities;
    using RstiNotifierBot.Common.Model.Repositories;

    internal class ChatRepository : BaseRepository,
        IChatRepository
    {
        #region SQL Queries

        private const string InsertChatQuery =
            @"insert into chat values(@ChatId, @Username, @FirstName, @LastName, @Title, @Type)";

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

        public void Create(Chat item) => ExecuteQuery(InsertChatQuery, item);

        public IList<Chat> GetChats() => GetQueryResult<Chat>(GetChatsQuery);

        public Chat GetChatById(long chatId) =>
            GetQueryResult<Chat>(GetChatByIdQuery, new { chatId }).FirstOrDefault();

        public void Update(Chat item) => ExecuteQuery(UpdateChatQuery, item);

        public void Delete(long chatId) => ExecuteQuery(DeleteChatQuery, new { chatId });

        #endregion
    }
}
