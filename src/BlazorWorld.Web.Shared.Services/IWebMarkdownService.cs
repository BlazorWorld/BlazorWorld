using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebMarkdownService
    {
        Task<string> RenderAsync(string text);
    }
}
