using Business.Hangfire.Managers;
using System;

namespace HangfireJobs
{
    public static class FireJobs
    {
        public static void FireJob<TJob>(int id) where TJob : IScheduleJobManager
        {
            Hangfire.BackgroundJob.Schedule<TJob>(
                job => job.Process(id),
                TimeSpan.FromSeconds(10)
            );
        }
    }
}
