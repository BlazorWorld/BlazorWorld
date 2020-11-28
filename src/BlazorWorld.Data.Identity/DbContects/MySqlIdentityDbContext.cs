using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorWorld.Data.Identity.DbContexts
{
    public class MySqlIdentityDbContext : AppIdentityDbContext
    {
        public MySqlIdentityDbContext(
            DbContextOptions<MySqlIdentityDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
#if DEBUG_EF
            options.UseMySql("DataSource=");
#endif
        }
    }
}
