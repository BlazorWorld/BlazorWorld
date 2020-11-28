using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorWorld.Data.Identity.DbContexts
{
    public class SqliteIdentityDbContext : AppIdentityDbContext
    {
        public SqliteIdentityDbContext(
            DbContextOptions<SqliteIdentityDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
#if DEBUG_EF
            options.UseSqlite("DataSource=");
#endif
        }
    }
}
