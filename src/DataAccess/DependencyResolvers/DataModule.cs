using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DependencyResolvers
{
    public class DataModule : ICoreModule
    {
        public void Load(IServiceCollection services) { }
    }
}
