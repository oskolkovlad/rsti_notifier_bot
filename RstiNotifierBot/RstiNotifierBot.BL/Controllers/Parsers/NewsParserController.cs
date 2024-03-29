﻿namespace RstiNotifierBot.BL.Controllers.Parsers
{
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;
    using RstiNotifierBot.BL.Constants;
    using RstiNotifierBot.Common.Model.Entities;

    internal class NewsParserController : HtmlParserController<News>
    {
        #region HtmlParserController Members

        protected override IEnumerable<News> GetItems(HtmlDocument document) =>
            document.DocumentNode.SelectNodes(NewsElements.News).Take(15).Select(GetItem);

        protected override News GetItem(HtmlNode node)
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

            return string.IsNullOrEmpty(title) && string.IsNullOrEmpty(preview) && string.IsNullOrEmpty(url)
                ? null
                : new News(title, preview, date, url, imageUrl);
        }

        #endregion
    }
}
