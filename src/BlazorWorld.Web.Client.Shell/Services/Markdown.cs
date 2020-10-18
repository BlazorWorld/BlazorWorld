using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Services
{
    public class Markdown : IMarkdown
    {
        private IJSRuntime JsRuntime { get; set; }

        public Markdown(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public async Task<string> RenderAsync(string text)
        {
            return await JsRuntime.InvokeAsync<string>("markdownRender", text);
        }
    }
}
