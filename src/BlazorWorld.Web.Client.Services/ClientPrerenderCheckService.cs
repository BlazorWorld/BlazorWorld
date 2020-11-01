using BlazorWorld.Web.Shared.Services;

namespace BlazorWorld.Web.Client.Services
{
    public class ClientPrerenderCheckService : IWebPrerenderCheckService
    {
        public bool IsPrerender
        {
            get => false;
        }
    }
}
