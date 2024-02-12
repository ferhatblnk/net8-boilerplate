using System.Threading.Tasks;

namespace Business.Hangfire.Managers.ScheduleJobs
{
    public class SampleScheduleJobManager : IScheduleJobManager
    {
        public Task Process(int id)
        {
            return Task.FromResult(id);
        }
    }
}
