using BlazorWorld.Web.Shared.Services;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerPrerenderCheckService : IWebPrerenderCheckService
    {
        public bool IsPrerender
        {
            get => true;
        }
    }
}
