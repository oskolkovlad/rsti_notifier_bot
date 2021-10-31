namespace RstiNotifierBot.Interfaces.Controllers.Handlers
{
    using RstiNotifierBot.Model.Entities;

    internal interface ISubscriptionHandler : IHandler
    {
        string Subscribe(long chatId);

        string Unsubscribe(long chatId);
    }
}