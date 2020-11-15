using BlazorWorld.Web.Client.Services;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;

namespace BlazorWorld.Web.Client.Shell
{
    public static class Extensions
    {
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

        public static bool IsAuthenticated(this AuthenticationState authenticationState)
        {
            var user = authenticationState.User;
            return user.Identity.IsAuthenticated;
        }

        public static string ToSlug(this string name)
        {
            string output = name;
            if (!string.IsNullOrEmpty(name))
            {
                output = output.ToLower();
                output = output.Replace(" ", "-");
                output = output.Replace("/", "-");
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                output = rgx.Replace(output, "");
            }

            return output;
        }
    }
}
