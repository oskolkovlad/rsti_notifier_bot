﻿namespace RstiNotifierBot.Controllers.Handlers
{
    using System;
    using RstiNotifierBot.Extensions;
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;
    using RstiNotifierBot.Model.Entities;
    using RstiNotifierBot.Properties;

    internal class SubscriptionHandler : ISubscriptionHandler
    {
        private const string SubscribedMessage ="Подписка оформлена!";
        private const string UnsubscribedMessage = "Вы отписались от новостной рассылки. Будем ждать вас еще!";
        private const string AlreadySubscribedMessage =
            "Вы уже оформили подписку.\nКак только появятся новости, мы сообщим, не переживайте)";
        private const string AlreadyUnsubscribedMessage = "Ваша подписка уже была отменена ранее.";

        private readonly IBCChatProperty _bcChatProperty;

        public SubscriptionHandler(IBCChatProperty bcChatProperty)
        {
            _bcChatProperty = bcChatProperty;
        }

        #region ISubscriptionHandler Members

        public string Subscribe(long chatId)
        {
            string message;

            if (IsSubscriptionAlreadyDone(chatId))
            {
                message = AlreadySubscribedMessage;
            }
            else
            {
                var chatProperty = CreateSubscriptionProperty(chatId);
                _bcChatProperty.Create(chatProperty);

                message = SubscribedMessage;
            }

            return message;
        }

        public string Unsubscribe(long chatId)
        {
            string message;

            if (!IsSubscriptionAlreadyDone(chatId))
            {
                message = AlreadyUnsubscribedMessage;
            }
            else
            {
                _bcChatProperty.Delete(chatId, Resources.SubscriptionPropertyName);
                message = UnsubscribedMessage;
            }

            return message;
        }

        #endregion

        #region Private Members

        private ChatProperty CreateSubscriptionProperty(long chatId)
        {
            var id = Guid.NewGuid().ToString().Clear("-");
            return new ChatProperty(id, chatId, Resources.SubscriptionPropertyName, true.ToString());
        }

        private bool IsSubscriptionAlreadyDone(long chatId) =>
            _bcChatProperty.IsExists(chatId, Resources.SubscriptionPropertyName, true.ToString());

        #endregion
    }
}
