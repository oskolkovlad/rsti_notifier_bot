namespace RstiNotifierBot.Model.Builders
{
    using RstiNotifierBot.Common.Model.Repositories;
    using RstiNotifierBot.Infrastructure;
    using RstiNotifierBot.Model.Repositories;

    public class ModelBuilder : BaseBuilder
    {
        #region BaseBuilder Members

        public override void Build(ComponentsContainer container)
        {
            container.Register<ChatRepository, IChatRepository>(true);
            container.Register<ChatPropertyRepository, IChatPropertyRepository>(true);
            container.Register<NewsRepository, INewsRepository>(true);
        }

        #endregion
    }
}
