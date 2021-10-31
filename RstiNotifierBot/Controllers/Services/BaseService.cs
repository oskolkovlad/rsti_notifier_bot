namespace RstiNotifierBot.Controllers.Services
{
    using RstiNotifierBot.Interfaces.Controllers.Services;

    internal abstract class BaseService : IService
    {
        private bool _isServiceStarted;

        #region IService Members

        public void Start()
        {
            if (_isServiceStarted)
            {
                return;
            }

            StartAction();
            _isServiceStarted = true;
        }

        public void Stop()
        {
            if (!_isServiceStarted)
            {
                return;
            }

            StopAction();
            _isServiceStarted = false;
        }

        #endregion

        #region Protected Members

        protected abstract void StartAction();

        protected abstract void StopAction();

        #endregion
    }
}
