namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using System;

    internal interface IBCSchedulerTasks
    {
        void ScheduleTask(long chatId, Action task, int dueTime = 0, int period = 300);

        void StopTask(long chatId);
    }
}
