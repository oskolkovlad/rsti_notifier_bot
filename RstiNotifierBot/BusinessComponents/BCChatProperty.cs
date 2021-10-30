namespace RstiNotifierBot.BusinessComponents
{
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Model.Repositories;
    using RstiNotifierBot.Model.Entities;

    internal class BCChatProperty : IBCChatProperty
    {
        private readonly IChatPropertyRepository _propertyRepository;

        public BCChatProperty(IChatPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        #region IBCChatProperty Members

        public void Create(ChatProperty chatProperty) => _propertyRepository.Create(chatProperty);

        public bool IsExists(long chatId, string name, string value) =>
            _propertyRepository.GetProperty(chatId, name, value) != null;

        public void Delete(long chatId, string name) => _propertyRepository.Delete(chatId, name);

        #endregion
    }
}
