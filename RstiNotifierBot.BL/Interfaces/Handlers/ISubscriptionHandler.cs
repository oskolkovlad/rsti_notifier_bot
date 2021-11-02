namespace RstiNotifierBot.BL.Interfaces.Handlers
{
    internal interface ISubscriptionHandler : IHandler
    {
        string Subscribe(long chatId);

        string Unsubscribe(long chatId);
    }
}