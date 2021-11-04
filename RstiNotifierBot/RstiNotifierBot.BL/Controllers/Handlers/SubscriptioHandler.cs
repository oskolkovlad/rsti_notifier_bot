namespace RstiNotifierBot.BL.Controllers.Handlers
{
    using System;
    using RstiNotifierBot.BL.Interfaces.Handlers;
    using RstiNotifierBot.BL.Properties;
    using RstiNotifierBot.Common.BC;
    using RstiNotifierBot.Common.BL.Extensions;
    using RstiNotifierBot.Common.Model.Entities;

    internal class SubscriptionHandler : ISubscriptionHandler
    {
        private const string SubscribedMessage = "Подписка оформлена!";
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
            string message = null;

            try
            {
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
            }
            catch (Exception exception)
            {
                exception.OutputConsoleLog();
            }

            return message;
        }

        public string Unsubscribe(long chatId)
        {
            string message = null;

            try
            {
                if (!IsSubscriptionAlreadyDone(chatId))
                {
                    message = AlreadyUnsubscribedMessage;
                }
                else
                {
                    _bcChatProperty.Delete(chatId, Resources.SubscriptionPropertyName);
                    message = UnsubscribedMessage;
                }
            }
            catch (Exception exception)
            {
                exception.OutputConsoleLog();
            }

            return message;
        }

        #endregion

        #region Private Members

        private ChatProperty CreateSubscriptionProperty(long chatId)
        {
            var id = Guid.NewGuid().ToString().Clear("-");
            return new ChatProperty(id, chatId, Resources.SubscriptionPropertyName, true.ToString().ToLower());
        }

        private bool IsSubscriptionAlreadyDone(long chatId) =>
            _bcChatProperty.IsExists(chatId, Resources.SubscriptionPropertyName, true.ToString().ToLower());

        #endregion
    }
}