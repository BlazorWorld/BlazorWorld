using BlazorWorld.Web.Server.Shell.Services;
using BlazorWorld.Web.Shared;
using BlazorWorld.Web.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWorld.Web.Server.Shell
{
    public static class Extensions
    {
        public static void AddBlazorWorldWebServerShellServices(this IServiceCollection services)
        {
            services.AddTransient<IWebConfigurationService, ServerConfigurationService>();
            services.AddTransient<IWebSecurityService, ServerSecurityService>();
        }
    }
}
