namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using System.Threading.Tasks;

    internal interface IBCChatProperty
    {
        Task<bool> HasSubcribtion(long chaId);

        Task Subscribe(long chaId);

        Task Unsubscribe(long chatId);
    }
}
