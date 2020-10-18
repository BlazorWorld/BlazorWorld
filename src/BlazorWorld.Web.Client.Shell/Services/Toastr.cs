using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Services
{
    public class Toastr : IToastr
    {
        private IJSRuntime JsRuntime { get; set; }

        public Toastr(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public async Task InfoAsync(string message)
        {
            await JsRuntime.InvokeAsync<string>("toastrInfo", message);
        }
        public async Task SuccessAsync(string message)
        {
            await JsRuntime.InvokeAsync<string>("toastrSuccess", message);
        }

        public async Task WarningAsync(string message)
        {
            await JsRuntime.InvokeAsync<string>("toastrWarning", message);
        }

        public async Task ErrorAsync(string message)
        {
            await JsRuntime.InvokeAsync<string>("toastrError", message);
        }

        public async Task RemoveAsync()
        {
            await JsRuntime.InvokeAsync<string>("toastrRemove");
        }

        public async Task ClearAsync()
        {
            await JsRuntime.InvokeAsync<string>("toastrClear");
        }

    }
}
