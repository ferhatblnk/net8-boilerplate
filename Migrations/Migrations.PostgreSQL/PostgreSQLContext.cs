using Core.DependencyInjection;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Migrations.PostgreSQL
{
    public class PostgreSQLContext : AppDataContext, ISelfScopedLifetime
    {
        public PostgreSQLContext(DbContextOptions options) : base(options)
        {
        }
    }
}