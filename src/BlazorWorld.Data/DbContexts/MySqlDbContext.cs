using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorWorld.Data.DbContexts
{
    public class MySqlDbContext : AppDbContext
    {
        public MySqlDbContext(
            DbContextOptions<MySqlDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
#if DEBUG_EF
            options.UseMySql("DataSource=", MySqlServerVersion.LatestSupportedServerVersion);
#endif
        }
    }
}
