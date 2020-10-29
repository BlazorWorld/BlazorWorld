using BlazorWorld.Web.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWorld.Web.Server.Services
{
    public static class Extensions
    {
        public static void AddBlazorWorldWebServerServices(this IServiceCollection services)
        {
            services.AddTransient<IWebConfigurationService, ServerConfigurationService>();
            services.AddTransient<IWebNodeService, ServerNodeService>();
            services.AddTransient<IWebSecurityService, ServerSecurityService>();
            services.AddTransient<IWebUserService, ServerUserService>();
        }
    }
}
