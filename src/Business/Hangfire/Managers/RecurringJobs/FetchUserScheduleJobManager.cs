using Business.Abstract;
using Core.Utilities.IoC;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Constants;
using Core.Extensions;
using Core.Settings.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Business.Hangfire.Managers.RecurringJobs
{
    public class FetchUserScheduleJobManager
    {
        public FetchUserScheduleJobManager()
        {
        }

        public Task Process()
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

            return Task.FromResult("");
        }
    }
}
