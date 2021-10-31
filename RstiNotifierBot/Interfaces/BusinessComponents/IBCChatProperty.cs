namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using System.Collections.Generic;
    using RstiNotifierBot.Model.Entities;

    internal interface IBCChatProperty : IBCComponent
    {
        void Create(ChatProperty item);

        IEnumerable<ChatProperty> GetProperties(string name, string value);

        bool IsExists(long chatId, string name, string value);

        void Delete(long chatId, string name);
    }
}
