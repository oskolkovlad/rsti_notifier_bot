namespace RstiNotifierBot.Common.BC
{
    using System.Collections.Generic;
    using RstiNotifierBot.Common.Model.Entities;

    public interface IBCChatProperty : IBCComponent
    {
        void Create(ChatProperty item);

        IEnumerable<ChatProperty> GetProperties(string name, string value);

        bool IsExists(long chatId, string name, string value);

        void Delete(long chatId, string name);
    }
}
