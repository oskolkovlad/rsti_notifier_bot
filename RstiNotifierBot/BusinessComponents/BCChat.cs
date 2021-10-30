namespace RstiNotifierBot.BusinessComponents
{
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

        public void Create(Chat chat) => _chatRepository.Create(chat);

        public bool IsExists(long chaId) => _chatRepository.GetChatById(chaId) != null;

        public void Delete(long chatId) => _chatRepository.Delete(chatId);

        #endregion
    }
}
