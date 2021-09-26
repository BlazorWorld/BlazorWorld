using BlazorWorld.Web.Client.Modules.Articles.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWorld.Web.Client.Modules.Articles.Components
{
    public partial class ArticleCard : ComponentBase
    {
        [Parameter]
        public Article Article { get; set; }
    }
}
