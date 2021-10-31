namespace RstiNotifierBot.BusinessComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Controllers.Parsers;
    using RstiNotifierBot.Model.Entities;

    internal class BCNewsList : IBCNewsList
    {
        private readonly IParserController<News> _parserController;

        public BCNewsList(IParserController<News> parserController)
        {
            _parserController = parserController;
        }

        #region IBCNewsList Members

        public async Task<News> GetLastNewsItem(string url)
        {
            var items = await GetNewsItems(url);
            return items.LastOrDefault();
        }

        public async Task<IEnumerable<News>> GetNewsItems(string url, bool reverse = true)
        {
            try
            {
                var items = await _parserController.Parse(url);
                return reverse ? items.Reverse() : items;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        #endregion
    }
}
