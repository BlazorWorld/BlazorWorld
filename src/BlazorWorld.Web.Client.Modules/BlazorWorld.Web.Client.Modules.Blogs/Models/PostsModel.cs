using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Modules.Common.Models;
using BlazorWorld.Web.Client.Modules.Common.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Blogs.Models
{
    public class PostsModel : NodesModel
    {
        public PostsModel(INodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Post> Items()
        {
            return Data.ConvertTo<Post>();
        }
    }
}
