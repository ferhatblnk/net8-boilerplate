using Core.Constants;
using Core.Extensions;
using Core.Settings.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Migrations.PostgreSQL;
using Migrations.SQLServer;
using System;
using System.IO;

namespace WebAPI.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = CreateAppDataContext(scope.ServiceProvider))
            {
                try
                {
                    //apply migrations
                    context.Database.Migrate();
                }
                catch (Exception ex) { }

                foreach (var file in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "wwwroot", "Infrastructure", "Functions"), "*.sql"))
                {
                    context.Database.ExecuteSqlRaw(File.ReadAllText(file), []);
                }

                foreach (var file in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "wwwroot", "Infrastructure", "Defaults"), "*.sql"))
                {
                    context.Database.ExecuteSqlRaw(File.ReadAllText(file), []);
                }
            }

            return app;
        }

        private static AppDataContext CreateAppDataContext(IServiceProvider serviceProvider)
        {
            var appSettings = serviceProvider.GetService<IOptionsSnapshot<AppSettings>>();

            var optionsBuilder = new DbContextOptionsBuilder();

            if (appSettings != null)
            {
                optionsBuilder.BuildAppDbContext(appSettings.Value);

                switch (appSettings.Value.DataProvider)
                {
                    case DataProvider.SQLSERVER:
                    return new SQLServerContext(/*optionsBuilder.Options*/);
                    case DataProvider.POSTGRESQL:
                    return new PostgreSQLContext(optionsBuilder.Options);
                    default:
                    {
                        throw new NotSupportedException($"{appSettings.Value.DataProvider} provider doesn't support.");
                    }
                }
            }

            throw new NotSupportedException("AppSettings doesn't exist.");
        }
    }
}
