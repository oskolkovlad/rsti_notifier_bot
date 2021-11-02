namespace RstiNotifierBot.Infrastructure.BC
{
    using RstiNotifierBot.BC.Builders;
    using RstiNotifierBot.Common.BC;
    using RstiNotifierBot.Infrastructure;

    public class BCContainer : ComponentsContainer
    {
        private static BCContainer _instance;

        #region Public Members

        public static BCContainer Instance
        {
            get { return _instance ?? (_instance = new BCContainer()); }
        }

        public T Get<T>() where T : IBCComponent => GetComponent<T>();

        #endregion

        #region ComponentsContainer Members

        protected override void RegisterBuilders(Director director) => director.Register(new BCBuilder());

        #endregion
    }
}
