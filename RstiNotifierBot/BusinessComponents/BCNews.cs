namespace RstiNotifierBot.BusinessComponents
{
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Model.Repositories;
    using RstiNotifierBot.Model.Entities;

    internal class BCNews : IBCNews
    {
        private readonly INewsRepository _newsRepository;

        public BCNews(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        #region IBCNewsProperty Members

        public News GetLastNewsItem()
        {
            return null;
        }

        #endregion
    }
}
