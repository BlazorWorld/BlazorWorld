using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Videos.Models
{
    public class ChannelsModel : NodesModel
    {
        public ChannelsModel(IWebNodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Models.Channel> Items()
        {
            return Data.ConvertTo<Models.Channel>();
        }
    }
}
