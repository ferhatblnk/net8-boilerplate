using Core.Extensions;
using Core.Settings.Concrete;
using Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Migrations.PostgreSQL
{
    public class PostgreSQLContextFactory : IDesignTimeDbContextFactory<PostgreSQLContext>
    {
        public PostgreSQLContext CreateDbContext(string[] args)
        {
            var appSettings = ServiceTool.ServiceProvider.GetService<IOptionsSnapshot<AppSettings>>();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.BuildAppDbContext(appSettings.Value);

            return new PostgreSQLContext(optionsBuilder.Options);
        }
    }
}