using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Services
{
    public class ClientTitleService : IWebTitleService
    {
        private IJSRuntime JsRuntime { get; set; }
        private string _title;

        public ClientTitleService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public string Title()
        {
            return _title;
        }

        public async Task SetPageTitleAsync(string title)
        {
            _title = title;
            await JsRuntime.InvokeVoidAsync("setTitle", title);
        }
    }
}
