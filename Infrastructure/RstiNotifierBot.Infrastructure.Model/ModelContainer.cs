namespace RstiNotifierBot.Infrastructure.Model
{
    using RstiNotifierBot.Common.Model.Repositories;
    using RstiNotifierBot.Infrastructure;
    using RstiNotifierBot.Model.Builders;

    public class ModelContainer : ComponentsContainer
    {
        private static ModelContainer _instance;

        #region Public Members

        public static ModelContainer Instance
        {
            get { return _instance ?? (_instance = new ModelContainer()); }
        }

        public T Get<T>() where T : IRepository => GetComponent<T>();

        #endregion

        #region ComponentsContainer Members

        protected override void RegisterBuilders(Director director) => director.Register(new ModelBuilder());

        #endregion
    }
}
