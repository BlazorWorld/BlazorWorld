using BlazorWorld.Web.Shared.Services;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Services
{
    public class ClientMomentService : IWebMomentService
    {
        private IJSRuntime JSRuntime { get; set; }

        public ClientMomentService(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
        }

        public async Task<string> FromNowAsync(string dateString)
        {
            if (!string.IsNullOrEmpty(dateString))
            {
                DateTimeOffset date;
                System.DateTimeOffset.TryParse(dateString, out date);
                return await JSRuntime.InvokeAsync<string>("fromNow", date.ToLocalTime().ToString("s"));
            }

            return string.Empty;
        }

        public async Task<string> LocalDateAsync(string dateString, string format)
        {
            if (!string.IsNullOrEmpty(dateString))
            {
                DateTimeOffset date;
                System.DateTimeOffset.TryParse(dateString, out date);
                return await JSRuntime.InvokeAsync<string>("localDate", date.ToLocalTime().ToString("s"), format);
            }

            return string.Empty;
        }
    }
}
