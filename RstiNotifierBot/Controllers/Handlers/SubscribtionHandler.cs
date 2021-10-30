namespace RstiNotifierBot.Controllers.Handlers
{
    using System.Threading.Tasks;
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;

    internal class SubscribtionHandler : ISubscribtionHandler
    {
        private const string SubscribedMessage ="Подписка оформлена!";
        private const string UnsubscribedMessage = "Вы отписались от новостной рассылки. Будем ждать вас еще!";
        private const string AlreadySubscribedMessage =
            "Вы уже оформили подписку.\nКак только появятся новости, мы сообщим, не переживайте)";
        private const string AlreadyUnsubscribedMessage = "Ваша подписка уже была отменена ранее.";

        private readonly IBCChat _bcChat;

        public SubscribtionHandler(IBCChat bcChat)
        {
            _bcChat = bcChat;
        }

        #region ISubscribtionHandler Members

        public async Task<string> Subscribe(long chatId)
        {
            string message;

            if (await IsSubcribtionAlreadyDone(chatId))
            {
                message = AlreadySubscribedMessage;
            }
            else
            {
                await _bcChat.Subscribe(chatId);
                message = SubscribedMessage;
            }

            return message;
        }

        public async Task<string> Unsubscribe(long chatId)
        {
            string message;

            if (!(await IsSubcribtionAlreadyDone(chatId)))
            {
                message = AlreadyUnsubscribedMessage;
            }
            else
            {
                await _bcChat.Unsubscribe(chatId);
                message = UnsubscribedMessage;
            }

            return message;
        }

        #endregion

        #region Private Members

        private async Task<bool> IsSubcribtionAlreadyDone(long chaId) => await _bcChat.HasSubcribtion(chaId);

        #endregion
    }
}
