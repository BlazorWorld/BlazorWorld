using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Modules.Common.Models;
using BlazorWorld.Web.Shared.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Forums.Models
{
    public class CommentsModel : NodesModel
    {
        public CommentsModel(IWebNodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Comment> Items()
        {
            return Data.ConvertTo<Models.Comment>();
        }
    }
}