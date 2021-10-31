namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using System;

    internal interface IBCSchedulerTasks : IBCComponent
    {
        void ScheduleTask(string taskId, Action task, int dueTime = 0, int period = 3600);

        void StopTask(string taskId);
    }
}
