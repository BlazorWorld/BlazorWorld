using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IMarkdownService
    {
        Task<string> RenderAsync(string text);
    }
}
