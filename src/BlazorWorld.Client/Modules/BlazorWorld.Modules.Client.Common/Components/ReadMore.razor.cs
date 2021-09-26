using Microsoft.AspNetCore.Components;

namespace BlazorWorld.Modules.Client.Common.Components
{
    public partial class ReadMore : ComponentBase
    {
        [Parameter]
        public string Link { get; set; }
    }
}
