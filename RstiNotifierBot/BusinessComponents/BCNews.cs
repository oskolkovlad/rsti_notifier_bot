namespace RstiNotifierBot.BusinessComponents
{
    using System.Collections.Generic;
    using System.Linq;
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

        public void Create(News item) => _newsRepository.Create(item);

        public IEnumerable<News> GetNewsItems() => _newsRepository.GetNews();

        public IEnumerable<News> GetLastNewsItems(int count = 15) =>
            _newsRepository.GetLastNews(count).Reverse();

        #endregion
    }
}
