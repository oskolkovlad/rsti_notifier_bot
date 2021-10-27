namespace RstiNotifierBot.BusinessComponents
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using RstiNotifierBot.Interfaces.BusinessComponents;

    internal class BCSchedulerTasks : IBCSchedulerTasks
    {
        private readonly IDictionary<long, Timer> _timers;

        private static readonly object _lockOblect = new object();
        
        public BCSchedulerTasks()
        {
            _timers = new Dictionary<long, Timer>();
        }

        #region IBCSchedulerJob Members

        public void ScheduleTask(long chatId, Action task, int dueTime = 0, int period = 300)
        {
            if (_timers.ContainsKey(chatId))
            {
                return;
            }

            var job = new Job(() => ExecuteWithDebug(task));
            var timer = new Timer(DoTimerJob, job, Timeout.Infinite, Timeout.Infinite);

            lock (_lockOblect)
            {
                _timers.Add(chatId, timer);
            }

            var dueTimeSpan = new TimeSpan(0, 0, dueTime);
            var periodSpan = new TimeSpan(0, 0, period);
            timer.Change(dueTimeSpan, periodSpan);
        }

        public void StopTask(long chatId)
        {
            if (!_timers.ContainsKey(chatId))
            {
                return;
            }

            var timer = _timers[chatId];

            lock (_lockOblect)
            {
                _timers.Remove(chatId);
            }

            timer.Change(Timeout.Infinite, Timeout.Infinite);
            timer.Dispose();
        }

        #endregion

        #region Private Members

        private void DoTimerJob(object item)
        {
            var job = item as Job;
            if (job == null)
            {
                throw new InvalidOperationException();
            }

            job.Task();
        }

        private static void ExecuteWithDebug(Action action)
        {
            try { action(); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #endregion

        private class Job
        {
            public Job(Action task)
            {
                Task = task;
            }

            public Action Task { get; private set; }
        }
    }
}
