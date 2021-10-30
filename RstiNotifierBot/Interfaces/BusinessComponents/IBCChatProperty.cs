namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RstiNotifierBot.Model.Entities;

    internal interface IBCChat
    {
        void CreateChat(Chat chat);

        Task<IEnumerable<Chat>> GetChats();

        Task<bool> HasSubcribtion(long chaId);

        Task Subscribe(long chaId);

        Task Unsubscribe(long chatId);
    }
}
