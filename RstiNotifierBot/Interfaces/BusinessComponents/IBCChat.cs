namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using RstiNotifierBot.Model.Entities;

    internal interface IBCChat
    {
        void Create(Chat chat);

        bool IsExists(long chaId);

        void Delete(long chatId);
    }
}
