using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorWorld.Modules.Client.Common.Services
{
    public class MomentService : IMomentService
    {
        private IJSRuntime JSRuntime { get; set; }

        public MomentService(IJSRuntime jsRuntime)
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
