using BlazorWorld.Web.Client.Messages.Services;
using BlazorWorld.Web.Shared;
using BlazorWorld.Web.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWorld.Web.Client.Messages
{
    public static class Extensions
    {
        public static void AddBlazorWorldWebMessagesServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IWebHubClientService, ClientHubClientService>();
            serviceCollection.AddTransient<IWebMessageService, ClientMessageService>();
        }
    }
}
