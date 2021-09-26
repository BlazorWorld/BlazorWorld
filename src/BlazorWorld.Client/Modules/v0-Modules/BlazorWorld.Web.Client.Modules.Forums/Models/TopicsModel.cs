using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Forums.Models
{
    public class TopicsModel : NodesModel
    {
        public TopicsModel(IWebNodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Topic> Items()
        {
            return Data.ConvertTo<Models.Topic>();
        }
    }
}