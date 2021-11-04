namespace RstiNotifierBot.Common.BC
{
    using RstiNotifierBot.Common.Model.Entities;

    public interface IBCChat : IBCComponent
    {
        void Create(Chat item);

        bool IsExists(long chaId);

        void Delete(long chatId);
    }
}
