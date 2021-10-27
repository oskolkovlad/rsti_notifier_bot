namespace RstiNotifierBot.Controllers.Parsers
{
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;

    internal class NewsParserController : HtmlParserController<NewsDto>
    {
        #region HtmlParserController Members

        protected override IEnumerable<NewsDto> GetItems(HtmlDocument document) =>
            document.DocumentNode.SelectNodes(NewsElements.News).Select(GetItem);

        protected override NewsDto GetItem(HtmlNode node)
        {
            var imageElement = node.SelectSingleNode(NewsElements.Image);
            var dateElement = node.SelectSingleNode(NewsElements.Date);
            var titleElement = node.SelectSingleNode(NewsElements.Title);
            var previewElement = node.SelectSingleNode(NewsElements.Preview);
            
            var title = titleElement?.InnerHtml;
            var preview = previewElement?.InnerHtml;
            var date = dateElement?.InnerHtml;
            var url = titleElement?.GetAttributeValue(NewsAttribute.Href, null);
            var imageUrl = imageElement?.GetAttributeValue(NewsAttribute.Source, null);

            return string.IsNullOrEmpty(title) && string.IsNullOrEmpty(preview)
                ? null
                : new NewsDto(title, preview, date, url, imageUrl);
        }

        #endregion
    }
}
