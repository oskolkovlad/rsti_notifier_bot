namespace RstiNotifierBot.BusinessComponents
{
    using System.Threading.Tasks;
    using RstiNotifierBot.Interfaces.BusinessComponents;

    internal class BCChatProperty : IBCChatProperty
    {
        #region IBCChatProperty Members

        public async Task<bool> HasSubcribtion(long chaId)
        {
            return false;
        }

        public async Task Subscribe(long chaId)
        {

        }

        public async Task Unsubscribe(long chatId)
        {

        }

        #endregion
    }
}
