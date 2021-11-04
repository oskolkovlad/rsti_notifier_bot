namespace RstiNotifierBot.Common.Model.Repositories
{
    using System.Collections.Generic;
    using RstiNotifierBot.Common.Model.Entities;

    public interface IChatPropertyRepository : IRepository
    {
        void Create(ChatProperty item);

        IList<ChatProperty> GetProperties(string name, string value);

        ChatProperty GetProperty(long chatId, string name, string value);

        void Delete(long chatId, string name);
    }
}
