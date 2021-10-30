namespace RstiNotifierBot.BusinessComponents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Model.Repositories;
    using RstiNotifierBot.Model.Entities;

    internal class BCChat : IBCChat
    {
        private readonly IChatRepository _chatRepository;

        public BCChat(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        #region IBCChatProperty Members

        public void CreateChat(Chat chat)
        {
            _chatRepository.Create(chat);
        }

        public async Task<IEnumerable<Chat>> GetChats()
        {
            return _chatRepository.GetChats();
        }

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
