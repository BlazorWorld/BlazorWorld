using BlazorWorld.Web.Client.Modules.Videos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWorld.Web.Client.Modules.Videos
{
    public static class Extensions
    {
        public static void AddBlazorWorldVideoServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IVideoService, VideoService>();
        }
    }
}
