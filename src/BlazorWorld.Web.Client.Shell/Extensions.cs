using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Web;

namespace BlazorWorld.Web.Client.Shell
{
    public static class Extensions
    {
        public static void AddBlazorWorldShellServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IConfigurationService, ConfigurationService>();
            serviceCollection.AddTransient<IInvitationService, InvitationService>();
            serviceCollection.AddTransient<IMarkdown, Markdown>();
            serviceCollection.AddTransient<IMoment, Moment>();
            serviceCollection.AddTransient<ISecurityService, SecurityService>();
            serviceCollection.AddTransient<IToastr, Toastr>();
        }

        public static Claim LoggedInUserClaim(this AuthenticationState authenticationState)
        {
            var user = authenticationState.User;
            if (!user.Identity.IsAuthenticated)
                return null;
            return user.FindFirst(c => c.Type == "sub");
        }

        public static string LoggedInUserId(this AuthenticationState authenticationState)
        {
            var loggedInUserClaim = authenticationState.LoggedInUserClaim();
            if (loggedInUserClaim != null)
                return loggedInUserClaim.Value;
            return string.Empty;
        }

        public static int StringToRows(this string value, int maxRows)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var rows = Math.Max(value.Split('\n').Length, value.Split('\r').Length);
                rows = Math.Max(rows, maxRows);
                return rows;
            }
            else
            {
                return maxRows;
            }
        }

        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        // get single querystring value with specified key
        public static string QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key];
        }
    }
}
