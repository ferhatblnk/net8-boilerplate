using Business.Abstract;
using Core.Utilities.IoC;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Business.Hangfire.Managers.FireJobs
{
    public class FetchUserFireJobManager : IScheduleJobManager
    {
        public FetchUserFireJobManager()
        {

        }

        public Task Process(int id)
        {
            // var context = ServiceTool.ServiceProvider.GetService<AppDataContext>();

            // var querySelect = $"";
            // var queryUpdate = $"";

            // context.Database.ExecuteSqlRaw(queryUpdate, []);

            // var users = context.Database
            //                 .SqlQuery<UserDto>(FormattableStringFactory.Create(querySelect))
            //                 .ToList();

            // foreach (var user in users)
            // {
            //     //
            // }

            return Task.FromResult(id);
        }
    }
}
