using BlazorWorld.Core.Repositories;
using BlazorWorld.Data.DbContexts;
using BlazorWorld.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorWorld.Data
{
    public static class Extensions
    {
        public static void AddBlazorWorldDataProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["AppDbProvider"];
            if (string.IsNullOrEmpty(provider))
            {
                provider = "sqlite";
            }
            string connectionString = configuration.GetConnectionString("AppDbConnection");
            switch (provider.ToLower())
            {
                case "sqlserver":
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString), ServiceLifetime.Transient);
                    services.AddDbContext<SqlServerDbContext>(options =>
                        options.UseSqlServer(connectionString), ServiceLifetime.Transient);
                    break;
                case "mysql":
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion),
                        ServiceLifetime.Transient);
                    services.AddDbContext<MySqlDbContext>(options =>
                        options.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion),
                        ServiceLifetime.Transient);
                    break;
                case "sqlite":
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        var appDbFilename = configuration["AppDbFilename"];
                        if (string.IsNullOrEmpty(appDbFilename))
                            appDbFilename = "blazorworld.db";
                        var connectionStringBuilder = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = appDbFilename };
                        connectionString = connectionStringBuilder.ToString();
                    }
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlite(connectionString), ServiceLifetime.Transient);
                    services.AddDbContext<SqliteDbContext>(options =>
                        options.UseSqlite(connectionString), ServiceLifetime.Transient);
                    break;
            }
        }

        public static void UpdateBlazorWorldDatabase(this IApplicationBuilder app, IConfiguration configuration)
        {
            var provider = configuration["AppDbProvider"].ToLower();
            if (string.IsNullOrEmpty(provider))
            {
                provider = "sqlite";
            }
            switch (provider.ToLower())
            {
                case "sqlite":
                    {
                        app.ProcessDb<SqliteDbContext>();
                        break;
                    }
                case "mysql":
                    {
                        app.ProcessDb<MySqlDbContext>();
                        break;
                    }
                case "sqlserver":
                    {
                        app.ProcessDb<SqlServerDbContext>();
                        break;
                    }
                default:
                    {
                        throw new Exception();
                    }
            }
        }

        private static void ProcessDb<T>(this IApplicationBuilder app) where T : AppDbContext
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<T>();
            context.Database.Migrate();
        }

        public static void AddBlazorWorldApplicationRepositories(this IServiceCollection services)
        {
            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<IEmailRepository, EmailRepository>();
            services.AddTransient<IInvitationRepository, InvitationRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<INodeRepository, NodeRepository>();
            services.AddTransient<ISettingRepository, SettingRepository>();
        }
    }
}
