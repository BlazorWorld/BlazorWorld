using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebTitleService
    {
        string Title();
        Task SetPageTitleAsync(string title);
    }
}
