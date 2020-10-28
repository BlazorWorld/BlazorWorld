using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Modules.Common.Models;
using BlazorWorld.Web.Shared.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Forums.Models
{
    public class ForumsModel : NodesModel
    {
        public ForumsModel(IWebNodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Forum> Items()
        {
            return Data.ConvertTo<Models.Forum>();
        }
    }
}
