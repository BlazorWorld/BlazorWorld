using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorWorld.Data.DbContexts
{
    public class SqlServerDbContext : AppDbContext
    {
        public SqlServerDbContext(
            DbContextOptions<SqlServerDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
#if DEBUG_EF
            options.UseSqlServer ("DataSource=");
#endif
        }
    }
}
