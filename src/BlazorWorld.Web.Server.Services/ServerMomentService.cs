using BlazorWorld.Web.Shared.Services;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerMomentService : IWebMomentService
    {
        public async Task<string> FromNowAsync(string dateString)
        {
            return dateString;
        }

        public async Task<string> LocalDateAsync(string dateString, string format)
        {
            return dateString;
        }
    }
}
