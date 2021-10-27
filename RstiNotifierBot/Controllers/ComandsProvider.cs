namespace RstiNotifierBot.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Interfaces.Controllers;
    using RstiNotifierBot.Interfaces.Controllers.Parsers;
    using RstiNotifierBot.Properties;

    internal class ComandsProvider : IComandsProvider
    {
        private readonly IParserController<NewsDto> parserController;

        public ComandsProvider(IParserController<NewsDto> parserController)
        {
            this.parserController = parserController;
        }

        #region IComandsProvider Members

        public AnswerResult GetAnswer(string command)
        {
            string answer;
            NewsDto newsItem = null;

            switch (command)
            {
                case Commands.Last:
                    newsItem = GetLastNews();
                    answer = ConstructAnswer(newsItem, false);
                    break;

                case Commands.Top5:
                    answer = ExecuteTop5Command();
                    break;

                case Commands.Info:
                    answer = Resources.Contacts;
                    break;

                case Commands.Subscribe:
                case Commands.Unsubscribe:
                    answer = null;
                    break;

                default:
                    return new AnswerResult();
            }

            return new AnswerResult(command, answer, newsItem);
        }

        #endregion

        #region Private Members

        private string ExecuteTop5Command()
        {
            string answer = null;

            var items = GetNewsItems().ToList();
            if (items.Any())
            {
                for (var i = 0; i < 4; i++)
                {
                    answer += ConstructAnswer(items[i]);
                    if (i != 4)
                    {
                        answer += Environment.NewLine;
                    }
                }
            }

            return answer;
        }

        private NewsDto GetLastNews() => GetNewsItems().FirstOrDefault();

        private IEnumerable<NewsDto> GetNewsItems() => parserController.Parse(Resources.NewsUrl);

        private static string ConstructAnswer(NewsDto item, bool appendLink = true)
        {
            var answer = new StringBuilder();
            answer.AppendLine(item?.Date);
            answer.AppendLine(item?.Title);
            answer.AppendLine(item?.Preview);
            if (appendLink)
            {
                answer.AppendLine(item?.Url);
            }

            return answer.ToString();
        }

        #endregion
    }
}
