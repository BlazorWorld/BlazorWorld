using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Modules.Blogs.Models;
using BlazorWorld.Web.Client.Modules.Common.Models;
using BlazorWorld.Web.Client.Modules.Common.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Blogs.Models
{
    public class BlogsModel : NodesModel
    {
        public BlogsModel(INodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Blog> Items()
        {
            return Data.ConvertTo<Blog>();
        }
    }
}
