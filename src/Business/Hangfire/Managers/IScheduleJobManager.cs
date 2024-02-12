using System.Threading.Tasks;

namespace Business.Hangfire.Managers
{
    public interface IScheduleJobManager
    {
        Task Process(int id);
    }
}
