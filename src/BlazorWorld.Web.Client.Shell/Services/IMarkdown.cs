using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Services
{
    public interface IMarkdown
    {
        Task<string> RenderAsync(string text);
    }
}
