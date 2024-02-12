using Core;
using Core.Constants;
using Core.Settings.Concrete;
using Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class AppDataContext : DbContext
    {
        public AppDataContext() : base() { }
        public AppDataContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var assembly in AssemblyContext.GetApplicationAssemblies())
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var appSettings = ServiceTool.ServiceProvider.GetService<IOptionsSnapshot<AppSettings>>();

                switch (appSettings.Value.DataProvider)
                {
                    case DataProvider.SQLSERVER:
                    {
                        optionsBuilder.UseSqlServer(appSettings.Value.ConnectionString, builder => { });
                        break;
                    }
                    case DataProvider.POSTGRESQL:
                    {
                        optionsBuilder.UseNpgsql(appSettings.Value.ConnectionString, builder => { });
                        break;
                    }
                    default:
                    {
                        throw new NotSupportedException($"{appSettings.Value.DataProvider} provider doesn't support.");
                    }
                }
            }
        }
    }
}
