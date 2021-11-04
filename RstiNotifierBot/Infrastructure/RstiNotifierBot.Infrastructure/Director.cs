namespace RstiNotifierBot.Infrastructure
{
    using System.Collections.Generic;

    public class Director
    {
        private readonly List<BaseBuilder> _builders;

        public Director()
        {
            _builders = new List<BaseBuilder>();
        }

        #region Public Members

        public void Register(params BaseBuilder[] builders) => _builders.AddRange(builders);

        public void Construct(ComponentsContainer container) => _builders.ForEach(x => x.Build(container));

        #endregion
    }
}
