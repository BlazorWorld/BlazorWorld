using BlazorWorld.Web.Client.Messages.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWorld.Web.Client.Messages
{
    public static class Extensions
    {
        public static void AddBlazorWorldWebMessagesServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<HubClientService>();
            serviceCollection.AddTransient<IMessageService, MessageService>();
        }
    }
}
