using BlazorWorld.Data;
using BlazorWorld.Data.Identity;
using BlazorWorld.Services;
using BlazorWorld.Web.Server.Messages.Services;
using BlazorWorld.Web.Server.Services;
using BlazorWorld.Web.Server.Services.Hubs;
using BlazorWorld.Web.Shared;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace BlazorWorld.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazorWorldIdentity(Configuration);
            services.AddBlazorWorldDataProvider(Configuration);

            services.AddAuthentication()
            .AddIdentityServerJwt();

            // BlazorWorld service additions
            // https://docs.microsoft.com/en-us/aspnet/core/security/blazor/webassembly/hosted-with-identity-server?view=aspnetcore-3.1&tabs=visual-studio
            services.Configure<IdentityOptions>(options =>
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
            services.AddHttpClient();
            services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Pages");
            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
            services.AddScoped<SignOutSessionStateManager>();

            services.AddBlazorWorldIdentityRepositories();
            services.AddBlazorWorldApplicationRepositories();
            services.AddBlazorWorldServices(Configuration);
            services.AddTransient<IWebHubClientService, ServerHubClientService>();
            services.AddTransient<IWebMessageService, ServerMessageService>();
            services.AddBlazorWorldWebServerServices();
            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddApiAuthorization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Blazorworld API",
                    Description = "Social Publishing System API",
                    TermsOfService = new Uri("https://blazorworld.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Jase Banico",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/jasebanico"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GPL",
                        Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.html"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddResponseCompression(opts => // for SignalR
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddRazorPages();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddResponseCaching();
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            // BlazorWorld configurations
            app.UseResponseCompression(); // for SignalR
            UseSwagger(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            var supportedCultures = new[] { "en-US", "fr" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();

            app.Use(async (ctx, next) =>
            {
                ctx.Request.Scheme = Configuration["SiteScheme"];
                ctx.Request.Host = new HostString(Configuration["SiteUrl"]);
                await next();
            });
            
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UpdateBlazorWorldDatabase(Configuration);
            app.UpdateBlazorWorldIdentityDatabase(Configuration);
            services.UseBlazorWorldServices(Configuration);

            app.UseResponseCaching();
            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(10)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Identity",
                    areaName: "Identity",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<MessagesHub>(Constants.MessagesHubPattern);

                // https://jonhilton.net/blazor-wasm-prerendering
                // endpoints.MapFallbackToFile("index.html");
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlazorWorld API V1");
            });
        }
    }
}
