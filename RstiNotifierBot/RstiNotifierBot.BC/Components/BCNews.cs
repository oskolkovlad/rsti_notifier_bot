namespace RstiNotifierBot.BC.Components
{
    using System.Collections.Generic;
    using System.Linq;
    using RstiNotifierBot.Common.BC;
    using RstiNotifierBot.Common.Model.Entities;
    using RstiNotifierBot.Common.Model.Repositories;
    using RstiNotifierBot.Infrastructure.Model;

    internal class BCNews : IBCNews
    {
        private readonly INewsRepository _newsRepository;

        public BCNews(INewsRepository newsRepository)
        {
            _newsRepository = ModelContainer.Instance.Get<INewsRepository>();
        }

        #region IBCNewsProperty Members

        public void Create(News item) => _newsRepository.Create(item);

        public IEnumerable<News> GetNewsItems() => _newsRepository.GetNews();

        public IEnumerable<News> GetLastNewsItems(int count = 15) =>
            _newsRepository.GetLastNews(count).Reverse();

        #endregion
    }
}
