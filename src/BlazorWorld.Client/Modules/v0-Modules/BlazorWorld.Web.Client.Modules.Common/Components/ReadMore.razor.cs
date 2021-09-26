using Microsoft.AspNetCore.Components;

namespace BlazorWorld.Web.Client.Modules.Common.Components
{
    public partial class ReadMore : ComponentBase
    {
        [Parameter]
        public string Link { get; set; }
    }
}
