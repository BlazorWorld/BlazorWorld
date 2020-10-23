using BlazorWorld.Web.Client.Modules.Videos.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWorld.Web.Client.Modules.Videos.Components
{
    public partial class ChannelCard : ComponentBase
    {
        [Parameter]
        public Channel Channel { get; set; }
    }
}
