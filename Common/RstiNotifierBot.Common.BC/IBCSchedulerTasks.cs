namespace RstiNotifierBot.Common.BC
{
    using System;

    public interface IBCSchedulerTasks : IBCComponent
    {
        void ScheduleTask(string taskId, Action task, int dueTime = 0, int period = 3600);

        void StopTask(string taskId);
    }
}
