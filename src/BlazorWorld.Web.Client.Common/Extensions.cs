using BlazorWorld.Web.Client.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWorld.Web.Client.Common
{
    public static class Extensions
    {
        public static void AddBlazorWorldCommonServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGroupService, GroupService>();
            serviceCollection.AddTransient<IUserApiService, UserApiService>();
        }
    }
}
