﻿namespace RstiNotifierBot.Controllers.Handlers
{
    using System;
    using System.Linq;
    using System.Text;
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;
    using RstiNotifierBot.Model.Entities;
    using RstiNotifierBot.Properties;

    internal class MessageHandler : IMessageHandler
    {
        private const string NewsNotFoundMessages = "На данный момент новостей нет, заходите еще...";

        private readonly IBCNewsList _bcNewsList;

        public MessageHandler(IBCNewsList bcNewsList)
        {
            _bcNewsList = bcNewsList;
        }

        #region Public Members

        public string GetNewsMessage(News item) =>
            item != null ? ConstructMessage(item, false) : NewsNotFoundMessages;

        public (string message, string url, string imageUrl) GetLastNewsMessage()
        {
            string message;

            var item = _bcNewsList.GetLastNewsItemAsync(Resources.NewsUrl).Result;
            message = item != null ? ConstructMessage(item, false) : NewsNotFoundMessages;

            return (message, item.Url, item.ImageUrl);
        }

        public string GetTop5NewsMessage()
        {
            string message = null;

            var items = _bcNewsList.GetNewsItemsAsync(Resources.NewsUrl, false).Result.ToList();
            if (items.Any())
            {
                for (var i = 0; i < 5; i++)
                {
                    var constructedAnswer = ConstructMessage(items[i]);
                    if (string.IsNullOrEmpty(constructedAnswer))
                    {
                        continue;
                    }

                    message += constructedAnswer;
                    if (i != 5)
                    {
                        message += Environment.NewLine;
                    }
                }
            }
            else
            {
                message = NewsNotFoundMessages;
            }

            return message;
        }

        public string GetContactInfoMessage() => Resources.Contacts;

        #endregion

        #region Private Members

        private static string ConstructMessage(News item, bool appendLink = true)
        {
            var message = new StringBuilder();
            message.AppendLine($"`{item?.PublishDate:dd.MM.yyyy}`");
            message.AppendLine($"*{item?.Title}*");
            message.AppendLine(item?.Preview);
            if (appendLink)
            {
                message.AppendLine(item?.Url);
            }

            return message.ToString();
        }
        
        #endregion
    }
}
