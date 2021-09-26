using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Articles.Models
{
    public class ArticlesModel : NodesModel
    {
        public ArticlesModel(IWebNodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Article> Items()
        {
            return Data.ConvertTo<Models.Article>();
        }
    }
}
