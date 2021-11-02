namespace RstiNotifierBot.BC.Components
{
    using RstiNotifierBot.Common.BC;
    using RstiNotifierBot.Common.Model.Entities;
    using RstiNotifierBot.Common.Model.Repositories;
    using RstiNotifierBot.Infrastructure.Model;

    internal class BCChat : IBCChat
    {
        private readonly IChatRepository _chatRepository;

        public BCChat(IChatRepository chatRepository)
        {
            _chatRepository = ModelContainer.Instance.Get<IChatRepository>();
        }

        #region IBCChatProperty Members

        public void Create(Chat item) => _chatRepository.Create(item);

        public bool IsExists(long chaId) => _chatRepository.GetChatById(chaId) != null;

        public void Delete(long chatId) => _chatRepository.Delete(chatId);

        #endregion
    }
}
