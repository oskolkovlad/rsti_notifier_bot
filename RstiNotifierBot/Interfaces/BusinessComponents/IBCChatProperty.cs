namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using RstiNotifierBot.Model.Entities;

    internal interface IBCChatProperty
    {
        void Create(ChatProperty chatProperty);

        bool IsExists(long chatId, string name, string value);

        void Delete(long chatId, string name);
    }
}
