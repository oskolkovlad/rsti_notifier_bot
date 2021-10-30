namespace RstiNotifierBot.Interfaces.Controllers.Handlers
{
    internal interface IMessageHandler
    {
        (string message, string url, string imageUrl) GetLastNewsMessage();

        string GetTop5NewsMessage();

        string GetContactInfoMessage();
    }
}