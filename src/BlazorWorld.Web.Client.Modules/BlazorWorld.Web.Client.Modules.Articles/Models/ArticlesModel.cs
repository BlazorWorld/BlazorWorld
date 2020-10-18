using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Modules.Common.Models;
using BlazorWorld.Web.Client.Modules.Common.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Articles.Models
{
    public class ArticlesModel : NodesModel
    {
        public ArticlesModel(INodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Article> Items()
        {
            return Data.ConvertTo<Models.Article>();
        }
    }
}
