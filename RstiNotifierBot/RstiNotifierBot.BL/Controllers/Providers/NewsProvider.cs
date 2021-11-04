namespace RstiNotifierBot.BL.Controllers.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RstiNotifierBot.BL.Interfaces.Parsers;
    using RstiNotifierBot.BL.Interfaces.Providers;
    using RstiNotifierBot.Common.BL.Extensions;
    using RstiNotifierBot.Common.Model.Entities;

    internal class NewsProvider : INewsProvider
    {
        private readonly IParserController<News> _parserController;

        public NewsProvider(IParserController<News> parserController)
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
