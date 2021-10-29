namespace RstiNotifierBot.Interfaces.Controllers.Handlers
{
    using System.Threading.Tasks;

    internal interface ISubscribtionHandler
    {
        Task Subscribe(long chatId);

        Task Unsubscribe(long chatId);
    }
}