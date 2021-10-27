namespace RstiNotifierBot.Interfaces.Controllers
{
    using RstiNotifierBot.Dto;

    internal interface IComandsProvider
    {
        AnswerResult GetAnswer(string command);
    }
}
