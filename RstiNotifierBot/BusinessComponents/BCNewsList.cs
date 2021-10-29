namespace RstiNotifierBot.BusinessComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Controllers.Parsers;

    internal class BCNewsList : IBCNewsList
    {
        private readonly IParserController<NewsDto> _parserController;

        public BCNewsList(IParserController<NewsDto> parserController)
        {
            _parserController = parserController;
        }

        #region IBCNewsList Members

        public async Task<NewsDto> GetLastNewsItem(string url)
        {
            var items = await GetNewsItems(url);
            return items.FirstOrDefault();
        }

        public async Task<IEnumerable<NewsDto>> GetNewsItems(string url)
        {
            try
            {
                var items = await _parserController.Parse(url);
                return items;
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
