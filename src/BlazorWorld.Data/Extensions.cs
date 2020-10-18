using BlazorWorld.Core.Repositories;
using BlazorWorld.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWorld.Data
{
    public static class Extensions
    {
        public static void AddBlazorWorldDataProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["DefaultApplicationDbProvider"];
            if (string.IsNullOrEmpty(provider))
            {
                provider = "sqlite";
            }
            string connectionString = configuration.GetConnectionString("DefaultApplicationDbConnection");
            switch (provider.ToLower())
            {
                case "mssql":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(connectionString));
                    break;
                case "mysql":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseMySql(connectionString));
                    break;
                case "sqlite":
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        var applicationDbFilename = configuration["ApplicationDbFilename"];
                        if (string.IsNullOrEmpty(applicationDbFilename))
                            applicationDbFilename = "blazorworld.db";
                        var connectionStringBuilder = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = applicationDbFilename };
                        connectionString = connectionStringBuilder.ToString();
                    }
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlite(connectionString));
                    break;
            }
        }

        public static void UpdateBlazorWorldApplicationDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }

        public static void AddBlazorWorldApplicationRepositories(this IServiceCollection services)
        {
            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IInvitationRepository, InvitationRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<INodeRepository, NodeRepository>();
        }
    }
}
