namespace RstiNotifierBot.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.CustomEventArgs;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Extensions;
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Controllers;
    using RstiNotifierBot.Interfaces.Controllers.Parsers;
    using RstiNotifierBot.Model;
    using RstiNotifierBot.Properties;

    internal class ComandsProvider : IComandsProvider
    {
        private readonly IParserController<NewsDto> parserController;
        private readonly IBCSchedulerTasks bcSchedulerTasks;

        public ComandsProvider(IParserController<NewsDto> parserController, IBCSchedulerTasks bcSchedulerTasks)
        {
            this.parserController = parserController;
            this.bcSchedulerTasks = bcSchedulerTasks;
        }

        #region IComandsProvider Members

        public event NewsEventHandler NotifyUser = delegate { };

        public AnswerResult GetAnswer(string command, long chatId)
        {
            string answer = null;
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
                    //bcSchedulerTasks.ScheduleTask(chatId, () => CheckNewsUpdate(chatId));
                    break;

                case Commands.Unsubscribe:
                    //UnubscribeFromNews(chatId);
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
            if (items.Count > 0)
            {
                for (var i = 0; i < 4; i++)
                {
                    var constructedAnswer = ConstructAnswer(items[i]);
                    if (string.IsNullOrEmpty(constructedAnswer))
                    {
                        continue;
                    }

                    answer += constructedAnswer;
                    if (i != 4)
                    {
                        answer += Environment.NewLine;
                    }
                }
            }

            return answer;
        }

        #region Subscribe/unsubscribe

        private void CheckNewsUpdate(long chatId)
        {
            var lastNewsItem = GetLastNews();
            if (lastNewsItem == null)
            {
                return;
            }

            try
            {
                if (IsLastNewsAdded(chatId, lastNewsItem))
                {
                    var args = new NewsEventArgs(chatId, lastNewsItem);
                    NotifyUser(this, args);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool IsLastNewsAdded(long chatId, NewsDto lastNewsItem)
        {
            using (var context = new NewsContext())
            {
                var news = context.News;
                var newsHistories = context.NewsHistories;

                var ourHistory = newsHistories.Where(x => x.ChatId == chatId).ToList();
                if (ourHistory.Count > 0)
                {
                    var existsNewsItem = news.FirstOrDefault(x =>
                        x.Title == lastNewsItem.Title &&
                        x.Date == lastNewsItem.Date &&
                        x.Url == lastNewsItem.Url);
                    if (existsNewsItem != null)
                    {
                        if (newsHistories.Any(x => x.NewsId == existsNewsItem.Id))
                        {
                            return false;
                        }
                        else
                        {
                            context.NewsHistories.Add(CreateNewsHistory(chatId, existsNewsItem.Id));
                        }
                    }
                }
                else
                {
                    var newNewsItem = CreateNews(lastNewsItem);
                    context.News.Add(newNewsItem);

                    context.NewsHistories.Add(CreateNewsHistory(chatId, newNewsItem.Id));
                }

                context.SaveChanges();
            }

            return true;
        }

        private News CreateNews(NewsDto newsItem)
        {
            var newNewsItem = new News
            {
                Id = Guid.NewGuid().ToString().Clear("-"),
                Title = newsItem.Title,
                Preview = newsItem.Preview,
                Date = newsItem.Date,
                Url = newsItem.Url
            };
            return newNewsItem;
        }

        private NewsHistory CreateNewsHistory(long chatId, string newsId)
        {
            var newsHistory = new NewsHistory
            {
                Id = Guid.NewGuid().ToString().Clear("-"),
                ChatId = chatId,
                NewsId = newsId
            };
            return newsHistory;
        }

        private void UnubscribeFromNews(long chatId)
        {
            //bcSchedulerTasks.StopTask(chatId);
        }

        #endregion

        private NewsDto GetLastNews() => GetNewsItems().FirstOrDefault();

        private IEnumerable<NewsDto> GetNewsItems()
        {
            try
            {
                return parserController.Parse(Resources.NewsUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        private static string ConstructAnswer(NewsDto item, bool appendLink = true)
        {
            if (item == null)
            {
                return null;
            }

            var answer = new StringBuilder();
            answer.AppendLine(item?.Date.ToShortDateString());
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
