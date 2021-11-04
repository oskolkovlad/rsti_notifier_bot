namespace RstiNotifierBot.BL.Services
{
    using System;
    using RstiNotifierBot.Common.BL.Extensions;
    using RstiNotifierBot.Common.BL.Services;

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

            try
            {
                StartAction();
                _isServiceStarted = true;
            }
            catch (Exception exception)
            {
                exception.OutputConsoleLog();
            }
        }

        public void Stop()
        {
            if (!_isServiceStarted)
            {
                return;
            }

            try
            {
                StopAction();
                _isServiceStarted = false;
            }
            catch (Exception exception)
            {
                exception.OutputConsoleLog();
            }
        }

        #endregion

        #region Protected Members

        protected abstract void StartAction();

        protected abstract void StopAction();

        #endregion
    }
}
