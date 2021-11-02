namespace RstiNotifierBot.BC.Builders
{
    using RstiNotifierBot.BC.Components;
    using RstiNotifierBot.Common.BC;
    using RstiNotifierBot.Common.Model.Repositories;
    using RstiNotifierBot.Infrastructure;
    using RstiNotifierBot.Infrastructure.BO.Dto;
    using RstiNotifierBot.Infrastructure.Model;

    public class BCBuilder : BaseBuilder
    {
        private const string ChatRepositoryParameterName = "chatRepository";
        private const string ChatPropertyRepositoryParameterName = "propertyRepository";
        private const string NewsRepositoryParameterName = "newsRepository";

        #region BaseBuilder Members

        #endregion
        public override void Build(ComponentsContainer container)
        {
            // Repositories.
            var chatRepository = ModelContainer.Instance.Get<IChatRepository>();
            var chatPropertyRepository = ModelContainer.Instance.Get<IChatPropertyRepository>();
            var newsRepository = ModelContainer.Instance.Get<INewsRepository>();

            // Business Components.
            container.Register<BCChat, IBCChat>(true,
                new RegisterParameter(ChatRepositoryParameterName, chatRepository));
            container.Register<BCChatProperty, IBCChatProperty>(true,
                new RegisterParameter(ChatPropertyRepositoryParameterName, chatPropertyRepository));
            container.Register<BCNews, IBCNews>(true,
                new RegisterParameter(NewsRepositoryParameterName, newsRepository));
            container.Register<BCSchedulerTasks, IBCSchedulerTasks>(true);
        }
    }
}
