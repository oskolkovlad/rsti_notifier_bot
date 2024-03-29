﻿namespace RstiNotifierBot.BL.Controllers.Handlers
{
    using System;
    using System.Linq;
    using System.Text;
    using RstiNotifierBot.BL.Interfaces.Handlers;
    using RstiNotifierBot.BL.Interfaces.Providers;
    using RstiNotifierBot.BL.Properties;
    using RstiNotifierBot.Common.Model.Entities;

    internal class MessageHandler : IMessageHandler
    {
        private const string NewsNotFoundMessages = "На данный момент новостей нет, заходите еще...";

        private readonly INewsProvider _newsProvider;

        public MessageHandler(INewsProvider newsProvider)
        {
            _newsProvider = newsProvider;
        }

        #region Public Members

        public string GetNewsMessage(News item) =>
            item != null ? ConstructMessage(item, false) : NewsNotFoundMessages;

        public (string message, string url, string imageUrl) GetLastNewsMessage()
        {
            string message;

            var item = _newsProvider.GetLastNewsItemAsync(Resources.NewsUrl).Result;
            message = item != null ? ConstructMessage(item, false) : NewsNotFoundMessages;

            return (message, item.Url, item.ImageUrl);
        }

        public string GetTop5NewsMessage()
        {
            string message = null;

            var items = _newsProvider.GetNewsItemsAsync(Resources.NewsUrl, false).Result.ToList();
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
