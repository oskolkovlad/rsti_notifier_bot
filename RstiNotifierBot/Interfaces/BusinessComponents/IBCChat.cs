namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using RstiNotifierBot.Model.Entities;

    internal interface IBCChat : IBCComponent
    {
        void Create(Chat item);

        bool IsExists(long chaId);

        void Delete(long chatId);
    }
}
