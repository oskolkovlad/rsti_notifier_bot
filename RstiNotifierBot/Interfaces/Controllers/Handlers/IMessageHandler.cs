namespace RstiNotifierBot.Interfaces.Controllers.Handlers
{
    using System.Threading.Tasks;

    internal interface IMessageHandler
    {
        Task<(string message, string url, string imageUrl)> GetLastNewsMessage();

        Task<string> GetTop5NewsMessage();

        string GetContactInfoMessage();
    }
}