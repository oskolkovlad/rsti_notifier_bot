namespace RstiNotifierBot.Dto
{
    internal class AnswerResult
    {
        public AnswerResult() { }

        public AnswerResult(string command, string answer, NewsDto newsItem = null)
        {
            Command = command;
            Answer = answer;
            NewsItem = newsItem;

            IsSuccess = true;
        }

        public bool IsSuccess { get; private set; }

        public string Command { get; private set; }

        public string Answer { get; private set; }

        public NewsDto NewsItem { get; private set; }
    }
}
