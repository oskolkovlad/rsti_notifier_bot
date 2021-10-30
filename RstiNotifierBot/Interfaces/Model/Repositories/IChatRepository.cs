namespace RstiNotifierBot.Interfaces.Model.Repositories
{
    using System.Collections.Generic;
    using RstiNotifierBot.Model.Entities;

    internal interface IChatRepository
    {
        void Create(Chat chat);

        IList<Chat> GetChats();

        Chat GetChatById(long chatId);

        void Update(Chat chat);

        void Delete(long chatId);
    }
}
