using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Web.Client.Modules.Videos.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Components
{
    public partial class VideoCard : ComponentBase
    {
        [Parameter]
        public Models.Video Video { get; set; }
    }
}
