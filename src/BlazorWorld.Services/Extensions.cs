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
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IInvitationService, InvitationService>();

            services.Configure<AuthMessageSenderOptions>(configuration);
        }

        public static void UseBlazorWorldServices(this IServiceProvider serviceProvider, IConfiguration configuration)
        {
            LoadSettingsAsync(serviceProvider, configuration).Wait();
            CreateUserRolesAsync(serviceProvider).Wait();
        }

        private static async Task LoadSettingsAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var settingService = serviceProvider.GetRequiredService<ISettingService>();
            await settingService.LoadSettingsAsync(configuration);
        }

        private static async Task CreateUserRolesAsync(IServiceProvider serviceProvider)
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

            var configurationService = serviceProvider.GetRequiredService<ISettingService>();
            var roleUsersArray = await configurationService.RoleUserSettingsAsync();
            foreach (var roleUserSettings in roleUsersArray)
            {
                var roleUsers = new RoleUsers(roleUserSettings);
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
    }
}
