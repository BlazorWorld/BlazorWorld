using BlazorWorld.Web.Shared.Services;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerTitleService : IWebTitleService
    {
        private string _title;

        public string Title()
        {
            return _title;
        }

        public async Task SetPageTitleAsync(string title)
        {
            _title = title;
        }
    }
}
