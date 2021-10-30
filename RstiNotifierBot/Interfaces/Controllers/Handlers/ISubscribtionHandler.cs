namespace RstiNotifierBot.Interfaces.Controllers.Handlers
{
    using System.Threading.Tasks;

    internal interface ISubscribtionHandler
    {
        Task<string> Subscribe(long chatId);

        Task<string> Unsubscribe(long chatId);
    }
}