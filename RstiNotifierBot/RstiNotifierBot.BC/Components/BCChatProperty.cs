namespace RstiNotifierBot.BC.Components
{
    using System.Collections.Generic;
    using RstiNotifierBot.Common.BC;
    using RstiNotifierBot.Common.Model.Entities;
    using RstiNotifierBot.Common.Model.Repositories;
    using RstiNotifierBot.Infrastructure.Model;

    internal class BCChatProperty : IBCChatProperty
    {
        private readonly IChatPropertyRepository _propertyRepository;

        public BCChatProperty(IChatPropertyRepository propertyRepository)
        {
            _propertyRepository = ModelContainer.Instance.Get<IChatPropertyRepository>();
        }

        #region IBCChatProperty Members

        public void Create(ChatProperty item) => _propertyRepository.Create(item);

        public IEnumerable<ChatProperty> GetProperties(string name, string value) =>
            _propertyRepository.GetProperties(name, value);

        public bool IsExists(long chatId, string name, string value) =>
            _propertyRepository.GetProperty(chatId, name, value) != null;

        public void Delete(long chatId, string name) => _propertyRepository.Delete(chatId, name);

        #endregion
    }
}
