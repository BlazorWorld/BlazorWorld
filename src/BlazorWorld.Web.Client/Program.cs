using BlazorWorld.Web.Client.Common;
using BlazorWorld.Web.Client.Messages;
using BlazorWorld.Web.Client.Modules.Common;
using BlazorWorld.Web.Client.Modules.Videos;
using BlazorWorld.Web.Client.Shell;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("BlazorWorld.Web.ServerAPI",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient("BlazorWorld.Web.PublicServerAPI", 
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("BlazorWorld.Web.ServerAPI"));

            builder.Services.AddApiAuthorization()
                .AddAccountClaimsPrincipalFactory<CustomUserFactory>();

            // Start BlazorWorld.Web.Client Updates
            builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            builder.Services.AddBlazorWorldCommonServices();
            builder.Services.AddBlazorWorldMessagesServices();
            builder.Services.AddBlazorWorldModuleServices();
            builder.Services.AddBlazorWorldShellServices();

            // Add module services here
            builder.Services.AddBlazorWorldVideoServices();

            builder.Services.AddOidcAuthentication(options =>
            {
                //options.ProviderOptions.Authority = "https://localhost:5001/";
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("oidc", options.ProviderOptions);
            });
            // End BlazorWorld.Web.Client Updates

            await builder.Build().RunAsync();
        }
    }
}
