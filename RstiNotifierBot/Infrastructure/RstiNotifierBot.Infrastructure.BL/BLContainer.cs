namespace RstiNotifierBot.Infrastructure.BL
{
    using RstiNotifierBot.BL.Builders;
    using RstiNotifierBot.Common.BL;
    using RstiNotifierBot.Infrastructure;

    public class BLContainer : ComponentsContainer
    {
        private static BLContainer _instance;

        #region Public Members

        public static BLContainer Instance
        {
            get { return _instance ?? (_instance = new BLContainer()); }
        }

        public T Get<T>() where T : IComponent => GetComponent<T>();

        #endregion

        #region ComponentsContainer Members

        protected override void RegisterBuilders(Director director) => director.Register(new BLBuilder());

        #endregion
    }
}
