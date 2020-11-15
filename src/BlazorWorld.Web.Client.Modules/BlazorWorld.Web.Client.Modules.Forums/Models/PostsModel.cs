using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Forums.Models
{
    public class PostsModel : NodesModel
    {
        public PostsModel(IWebNodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Post> Items()
        {
            return Data.ConvertTo<Models.Post>();
        }
    }
}