using IdentityServer4.Configuration;
using BlazorWorld.Data.Identity.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace BlazorWorld.Data.Identity
{
    public static class Extensions
    {
        public static void AddBlazorWorldIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["IdentityDbProvider"];
            if (string.IsNullOrEmpty(provider))
            {
                provider = "sqlite";
            }
            string connectionString = configuration.GetConnectionString("IdentityDbConnection");
            switch (provider.ToLower())
            {
                case "mssql":
                    services.AddDbContext<ApplicationIdentityDbContext>(options =>
                        options.UseSqlServer(connectionString));
                    break;
                case "mysql":
                    services.AddDbContext<ApplicationIdentityDbContext>(options =>
                        options.UseMySql(connectionString));
                    break;
                case "sqlite":
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        var identityDbFilename = configuration["IdentityDbFilename"];
                        if (string.IsNullOrEmpty(identityDbFilename))
                            identityDbFilename = "blazorworld-identity.db";
                        var connectionStringBuilder = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = identityDbFilename };
                        connectionString = connectionStringBuilder.ToString();
                    }
                    services.AddDbContext<ApplicationIdentityDbContext>(options =>
                        options.UseSqlite(connectionString));
                    break;
            }

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationIdentityDbContext>();
        }

        public static void UpdateBlazorWorldIdentityDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ApplicationIdentityDbContext>();
            context.Database.Migrate();
        }

        public static void AddBlazorWorldIdentityRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
