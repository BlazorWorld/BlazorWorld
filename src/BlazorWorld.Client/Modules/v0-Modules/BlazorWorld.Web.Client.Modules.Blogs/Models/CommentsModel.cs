using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Blogs.Models
{
    public class CommentsModel : NodesModel
    {
        public CommentsModel(IWebNodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Comment> Items()
        {
            return Data.ConvertTo<Comment>();
        }
    }
}
