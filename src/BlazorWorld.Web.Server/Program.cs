using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BlazorWorld.Web.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Sources.Clear();

                    var env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                            optional: true, reloadOnChange: true);

                    config.AddJsonFile("Settings/content-appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"Settings/content-appsettings.{env.EnvironmentName}.json",
                            optional: true, reloadOnChange: true);

                    config.AddJsonFile("Settings/modules-appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"Settings/modules-appsettings.{env.EnvironmentName}.json",
                            optional: true, reloadOnChange: true);

                    config.AddJsonFile("Settings/security-appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"Settings/security-appsettings.{env.EnvironmentName}.json",
                            optional: true, reloadOnChange: true);

                    config.AddJsonFile("Settings/site-appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"Settings/site-appsettings.{env.EnvironmentName}.json",
                            optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
