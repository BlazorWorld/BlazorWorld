using BlazorWorld.Web.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWorld.Web.Server.Services
{
    public static class Extensions
    {
        public static void AddBlazorWorldWebServerServices(this IServiceCollection services)
        {
            services.AddTransient<IWebCategoryService, ServerCategoryService>();
            services.AddTransient<IWebConfigurationService, ServerConfigurationService>();
            services.AddTransient<IWebMarkdownService, ServerMarkdownService>();
            services.AddTransient<IWebMomentService, ServerMomentService>();
            services.AddTransient<IWebPrerenderCheckService, ServerPrerenderCheckService>();
            services.AddTransient<IWebNodeService, ServerNodeService>();
            services.AddTransient<IWebSecurityService, ServerSecurityService>();
            services.AddTransient<IWebUserService, ServerUserService>();
            services.AddTransient<IWebVoteService, ServerVoteService>();
        }
    }
}
