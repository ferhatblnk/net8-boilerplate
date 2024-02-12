using Business.Hangfire.Managers;
using System;

namespace HangfireJobs
{
    public static class ScheduleJobs
    {
        public static string Schedule<TJob>(int id, TimeSpan? timeSpan = null) where TJob : IScheduleJobManager
        {
            return Hangfire.BackgroundJob.Schedule<TJob>(
                job => job.Process(id),
                timeSpan ?? TimeSpan.FromSeconds(10)
            );
        }

        public static void RemoveSchedule(string id)
        {
            Hangfire.BackgroundJob.Delete(id);
        }

        public static string Enqueue<TJob>(int id) where TJob : IScheduleJobManager
        {
            return Hangfire.BackgroundJob.Enqueue<TJob>(
                job => job.Process(id)
            );
        }
    }
}
