using BlazorWorld.Modules.Client.Articles.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWorld.Modules.Client.Articles.Components
{
    public partial class ArticleCard : ComponentBase
    {
        [Parameter]
        public Article Article { get; set; }
    }
}
