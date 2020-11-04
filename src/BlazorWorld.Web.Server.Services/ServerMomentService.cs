using BlazorWorld.Web.Shared.Services;
using System;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerMomentService : IWebMomentService
    {
        public async Task<string> FromNowAsync(string dateString)
        {
            return DateTimeOffset.Parse(dateString).ToLocalTime().ToString("MMMM dd, yyyy");
        }

        public async Task<string> LocalDateAsync(string dateString, string format)
        {
            return string.Empty;
        }
    }
}
