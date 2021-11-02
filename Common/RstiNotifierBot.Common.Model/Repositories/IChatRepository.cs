namespace RstiNotifierBot.Common.Model.Repositories
{
    using System.Collections.Generic;
    using RstiNotifierBot.Common.Model.Entities;

    public interface IChatRepository : IRepository
    {
        void Create(Chat item);

        IList<Chat> GetChats();

        Chat GetChatById(long chatId);

        void Update(Chat item);

        void Delete(long chatId);
    }
}
