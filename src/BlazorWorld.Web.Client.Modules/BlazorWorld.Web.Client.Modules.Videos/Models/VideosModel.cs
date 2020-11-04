using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Modules.Common.Models;
using BlazorWorld.Web.Shared.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Videos.Models
{
    public class VideosModel : NodesModel
    {
        public VideosModel(IWebNodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Models.Video> Items()
        {
            return Data.ConvertTo<Models.Video>();
        }
    }
}
