using BlazorWorld.Web.Shared.Services;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerMarkdownService : IWebMarkdownService
    {
        public async Task<string> RenderAsync(string text)
        {
            return text;
        }
    }
}
