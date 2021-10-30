namespace RstiNotifierBot.Interfaces.Controllers.Handlers
{
    using RstiNotifierBot.Model.Entities;

    internal interface ISubscriptionHandler
    {
        string Subscribe(long chatId);

        string Unsubscribe(long chatId);
    }
}