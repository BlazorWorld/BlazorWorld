using BlazorWorld.Web.Client.Modules.Common.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace BlazorWorld.Web.Client.Modules.Common
{
    public static class Extensions
    {
        public static void AddModuleServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICategoryService, CategoryService>();
            serviceCollection.AddTransient<INodeService, NodeService>();
            serviceCollection.AddTransient<IVoteService, VoteService>();
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
