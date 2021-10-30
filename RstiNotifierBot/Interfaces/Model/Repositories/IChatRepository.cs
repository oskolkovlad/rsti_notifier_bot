namespace RstiNotifierBot.Interfaces.Model.Repositories
{
    using System.Collections.Generic;
    using RstiNotifierBot.Model.Entities;

    internal interface IChatRepository
    {
        IList<Chat> GetChats();

        Chat GetChatById(long chatId);

        void Create(Chat chat);

        void Update(Chat chat);

        void Delete(long chatId);
    }
}
