using BlazorWorld.Web.Client.Modules.Videos;
using BlazorWorld.Web.Client.Services;
using BlazorWorld.Web.Shared.Services;
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
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("BlazorWorld.Web.ServerAPI"));

            builder.Services.AddApiAuthorization()
                .AddAccountClaimsPrincipalFactory<CustomUserFactory>();

            // Start BlazorWorld.Web.Client Updates
            builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            builder.Services.AddBlazorWorldWebClientServices();

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

            var host = builder.Build();

            var hubClientService = host.Services.GetRequiredService<IWebHubClientService>();
            await hubClientService.InitAsync();

            await host.RunAsync();
        }
    }
}
