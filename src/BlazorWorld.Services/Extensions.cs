using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Data.Identity;
using BlazorWorld.Services.Configuration;
using BlazorWorld.Services.Configuration.Models;
using BlazorWorld.Services.Content;
using BlazorWorld.Services.Organization;
using BlazorWorld.Services.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BlazorWorld.Services
{
    public static class Extensions
    {
        public static void AddBlazorWorldServices(this IServiceCollection services, IConfiguration configuration)
        {
            // content
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<INodeService, NodeService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IUserService, UserService>();

            // organization
            services.AddTransient<IGroupService, GroupService>();

            // security
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IAppEmailSender, EmailSender>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IPermissionsService, PermissionsService>();
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IInvitationService, InvitationService>();

            services.Configure<AuthMessageSenderOptions>(configuration);
        }

        public static void UseBlazorWorldSecurity(this IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            CreateUserRolesAsync(serviceProvider, configuration).Wait();
            CreatePermissionsAsync(serviceProvider, configuration).Wait();
        }

        private static async Task CreateUserRolesAsync(IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in Roles.All)
            {
                IdentityResult roleResult;
                var roleCheck = await roleManager.RoleExistsAsync(role);
                if (!roleCheck)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var securityAppSettings = new SecurityAppSettings();
            configuration.Bind(nameof(SecurityAppSettings), securityAppSettings);

            var roleUsersArray = securityAppSettings.DefaultRoleUsers;
            foreach (var roleUsers in roleUsersArray)
            {
                foreach (var userName in roleUsers.Users)
                {
                    ApplicationUser user = await userManager.FindByNameAsync(userName);
                    if (user != null)
                    {
                        var inRole = await userManager.IsInRoleAsync(user, roleUsers.Role);
                        if (!inRole)
                            await userManager.AddToRoleAsync(user, roleUsers.Role);
                    }
                }
            }
        }

        private static async Task CreatePermissionsAsync(IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            var permissionsService = serviceProvider.GetRequiredService<IPermissionsService>();

            var securityAppSettings = new SecurityAppSettings();
            configuration.Bind(nameof(SecurityAppSettings), securityAppSettings);

            foreach (var permissionSetting in securityAppSettings.DefaultPermissions)
            {
                var permissionKeyValue = permissionSetting.Split(',');
                var resource = permissionKeyValue[0];
                var resourceFields = resource.Split(':');
                var module = resourceFields[0];
                var type = resourceFields[1];
                var action = permissionKeyValue[1];
                var role = permissionKeyValue[2];

                var found = await permissionsService.AllowedAsync(module, type, action, role, false);
                if (!found)
                {
                    var permission = new Permission()
                    {
                        Module = module,
                        Type = type,
                        Action = action,
                        Role = role
                    };

                    await permissionsService.AddAsync(permission);
                }
            }
        }
    }
}
