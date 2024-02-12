using Core.DependencyInjection;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace Migrations.SQLServer
{
    public class SQLServerContext : AppDataContext, ISelfScopedLifetime
    {
        public SQLServerContext() : base()
        {
        }
    }
}