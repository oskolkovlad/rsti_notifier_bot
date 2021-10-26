namespace RstiNotifierBot.Controllers.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using RstiNotifierBot.Interfaces.Controllers.Parsers;

    internal abstract class HtmlParserController<T> : IParserController<T>
    {
        #region IParserController Members

        public IEnumerable<T> Parse(string source)
        {
            var htmlContent = GetHtmlContent(source).Result;
            if (string.IsNullOrEmpty(htmlContent))
            {
                return null;
            }

            var document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            var items = GetItems(document);
            return items;
        }

        #endregion

        #region Protected Members

        protected abstract IEnumerable<T> GetItems(HtmlDocument document);

        protected abstract T GetItem(HtmlNode node);

        #endregion

        #region Private Members

        private static async Task<string> GetHtmlContent(string url)
        {
            try
            {
                using var handler = new HttpClientHandler();
                using var httpClient = new HttpClient(handler);
                using var response = await httpClient.GetAsync(url);
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    var htmlContent = await response.Content.ReadAsStringAsync();
                    return htmlContent;
                }
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
