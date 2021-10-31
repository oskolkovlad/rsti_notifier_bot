namespace RstiNotifierBot.Interfaces.Model.Repositories
{
    using System.Collections.Generic;
    using RstiNotifierBot.Model.Entities;

    internal interface IChatPropertyRepository
    {
        void Create(ChatProperty item);

        IList<ChatProperty> GetProperties(string name, string value);

        ChatProperty GetProperty(long chatId, string name, string value);

        void Delete(long chatId, string name);
    }
}
