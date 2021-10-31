namespace RstiNotifierBot.Interfaces.Controllers.Handlers
{
    using RstiNotifierBot.Model.Entities;

    internal interface IMessageHandler : IHandler
    {
        string GetNewsMessage(News item);

        (string message, string url, string imageUrl) GetLastNewsMessage();

        string GetTop5NewsMessage();

        string GetContactInfoMessage();
    }
}