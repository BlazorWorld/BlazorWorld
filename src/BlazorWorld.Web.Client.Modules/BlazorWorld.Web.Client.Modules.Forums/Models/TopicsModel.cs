using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Modules.Common.Models;
using BlazorWorld.Web.Client.Modules.Common.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Forums.Models
{
    public class TopicsModel : NodesModel
    {
        public TopicsModel(INodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Topic> Items()
        {
            return Data.ConvertTo<Models.Topic>();
        }
    }
}