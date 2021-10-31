namespace RstiNotifierBot.Interfaces.Model.Repositories
{
    using System.Collections.Generic;
    using RstiNotifierBot.Model.Entities;

    internal interface IChatRepository
    {
        void Create(Chat item);

        IList<Chat> GetChats();

        Chat GetChatById(long chatId);

        void Update(Chat item);

        void Delete(long chatId);
    }
}
