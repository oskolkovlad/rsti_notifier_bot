namespace RstiNotifierBot.Interfaces.Controllers
{
    using RstiNotifierBot.CustomEventArgs;
    using RstiNotifierBot.Dto;

    internal interface IComandsProvider
    {
        event NewsEventHandler NotifyUser;

        AnswerResult GetAnswer(string command, long chatId);
    }
}
