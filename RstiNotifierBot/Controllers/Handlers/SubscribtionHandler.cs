namespace RstiNotifierBot.Controllers.Handlers
{
    using System.Threading.Tasks;
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;

    internal class SubscribtionHandler : ISubscribtionHandler
    {
        private readonly IBCNewsList _bcNewsList;
        private readonly IBCSchedulerTasks _bcSchedulerTasks;

        public SubscribtionHandler(IBCNewsList bcNewsList, IBCSchedulerTasks bcSchedulerTasks)
        {
            _bcNewsList = bcNewsList;
            _bcSchedulerTasks = bcSchedulerTasks;
        }

        #region ISubscribtionHandler Members

        public Task Subscribe(long chatId)
        {
            throw new System.NotImplementedException();
        }

        public Task Unsubscribe(long chatId)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Private Members



        #endregion

        //private void CheckNewsUpdate(long chatId)
        //{
        //    var lastNewsItem = GetLastNews();
        //    if (lastNewsItem == null)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        if (IsLastNewsAdded(chatId, lastNewsItem))
        //        {
        //            var args = new NewsEventArgs(chatId, lastNewsItem);
        //            NotifyUser(this, args);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private bool IsLastNewsAdded(long chatId, NewsDto lastNewsItem)
        //{
        //    using (var context = new NewsContext())
        //    {
        //        var news = context.News;
        //        var newsHistories = context.NewsHistories;

        //        var ourHistory = newsHistories.Where(x => x.ChatId == chatId).ToList();
        //        if (ourHistory.Count > 0)
        //        {
        //            var existsNewsItem = news.FirstOrDefault(x =>
        //                x.Title == lastNewsItem.Title &&
        //                x.Date == lastNewsItem.Date &&
        //                x.Url == lastNewsItem.Url);
        //            if (existsNewsItem != null)
        //            {
        //                if (newsHistories.Any(x => x.NewsId == existsNewsItem.Id))
        //                {
        //                    return false;
        //                }
        //                else
        //                {
        //                    context.NewsHistories.Add(CreateNewsHistory(chatId, existsNewsItem.Id));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var newNewsItem = CreateNews(lastNewsItem);
        //            context.News.Add(newNewsItem);

        //            context.NewsHistories.Add(CreateNewsHistory(chatId, newNewsItem.Id));
        //        }

        //        context.SaveChanges();
        //    }

        //    return true;
        //}

        //private News CreateNews(NewsDto newsItem)
        //{
        //    var newNewsItem = new News
        //    {
        //        Id = Guid.NewGuid().ToString().Clear("-"),
        //        Title = newsItem.Title,
        //        Preview = newsItem.Preview,
        //        Date = newsItem.Date,
        //        Url = newsItem.Url
        //    };
        //    return newNewsItem;
        //}

        //private NewsHistory CreateNewsHistory(long chatId, string newsId)
        //{
        //    var newsHistory = new NewsHistory
        //    {
        //        Id = Guid.NewGuid().ToString().Clear("-"),
        //        ChatId = chatId,
        //        NewsId = newsId
        //    };
        //    return newsHistory;
        //}

        //private void UnubscribeFromNews(long chatId)
        //{
        //    //bcSchedulerTasks.StopTask(chatId);
        //}
    }
}
