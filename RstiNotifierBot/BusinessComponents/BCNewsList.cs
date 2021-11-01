namespace RstiNotifierBot.BusinessComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RstiNotifierBot.Extensions;
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

        public async Task<News> GetLastNewsItemAsync(string url)
        {
            var items = await GetNewsItemsAsync(url);
            return items.LastOrDefault();
        }

        public async Task<IEnumerable<News>> GetNewsItemsAsync(string url, bool reverse = true)
        {
            try
            {
                var items = await _parserController.ParseAsync(url);
                if (items == null)
                {
                    return Enumerable.Empty<News>();
                }

                return reverse ? items.Reverse() : items;
            }
            catch (Exception exception)
            {
                exception.OutputConsoleLog();
            }

            return null;
        }

        #endregion
    }
}
