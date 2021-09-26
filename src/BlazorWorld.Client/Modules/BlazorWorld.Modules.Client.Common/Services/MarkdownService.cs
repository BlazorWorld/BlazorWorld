using BlazorWorld.Web.Shared.Services;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Services
{
    public class ClientMarkdownService : IMarkdownService
    {
        private IJSRuntime JsRuntime { get; set; }

        public ClientMarkdownService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public async Task<string> RenderAsync(string text)
        {
            return await JsRuntime.InvokeAsync<string>("markdownRender", text);
        }
    }
}
