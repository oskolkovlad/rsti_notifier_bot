﻿namespace RstiNotifierBot.BC.Components
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using RstiNotifierBot.Common.BC;
    using RstiNotifierBot.Common.BL.Extensions;

    internal class BCSchedulerTasks : IBCSchedulerTasks
    {
        private readonly IDictionary<string, Timer> _timers;

        private static readonly object _lockObject = new object();
        
        public BCSchedulerTasks()
        {
            _timers = new Dictionary<string, Timer>();
        }

        #region IBCSchedulerJob Members

        public void ScheduleTask(string taskId, Action task, int dueTime = 0, int period = 3600)
        {
            lock (_lockObject)
            {
                if (_timers.ContainsKey(taskId))
                {
                    return;
                }
            }

            var job = new Job(() => ExecuteWithDebug(task));
            var timer = new Timer(DoTimerJob, job, Timeout.Infinite, Timeout.Infinite);

            lock (_lockObject)
            {
                _timers.Add(taskId, timer);
            }

            var dueTimeSpan = new TimeSpan(0, 0, dueTime);
            var periodSpan = new TimeSpan(0, 0, period);
            timer.Change(dueTimeSpan, periodSpan);
        }

        public void StopTask(string taskId)
        {
            Timer timer;
            lock (_lockObject)
            {
                if (!_timers.ContainsKey(taskId))
                {
                    return;
                }

                timer = _timers[taskId];
                _timers.Remove(taskId);
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
            catch (Exception exception)
            {
                exception.OutputConsoleLog();
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
