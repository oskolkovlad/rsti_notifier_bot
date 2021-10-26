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

    internal class AnswerProvider : IAnswerProvider
    {
        private readonly IParserController<NewsDto> parserController;

        public AnswerProvider(IParserController<NewsDto> parserController)
        {
            this.parserController = parserController;
        }

        #region IAnswerProvider Members

        public string GetAnswer(string message)
        {
            string answer = null;

            switch (message)
            {
                case Commands.Last:
                    var lastNews = GetLastNews();
                    answer = ConstructLastNewsAnswer(lastNews);
                    break;

                case Commands.Top5:
                    var items = GetNewsItems();
                    if (items.Any())
                    {
                        foreach (var item in items.Take(5))
                        {
                            answer += ConstructLastNewsAnswer(item);
                            answer += Environment.NewLine;
                        }

                        answer.TrimEnd('\n');
                    }
                    break;

                default:
                    return null;
            }

            return answer;
        }

        #endregion

        #region Private Members

        private NewsDto GetLastNews() => GetNewsItems().FirstOrDefault();

        private IEnumerable<NewsDto> GetNewsItems() => parserController.Parse(Resources.NewsUrl);

        private static string ConstructLastNewsAnswer(NewsDto item)
        {
            var answer = new StringBuilder();
            answer.AppendLine(item?.Date);
            answer.AppendLine(item?.Title);
            answer.AppendLine(item?.Preview);
            answer.AppendLine(item?.Xref);

            return answer.ToString();
        }

        #endregion
    }
}
