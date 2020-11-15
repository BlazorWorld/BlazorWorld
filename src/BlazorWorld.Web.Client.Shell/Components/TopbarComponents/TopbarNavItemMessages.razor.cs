using BlazorWorld.Web.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Components.TopbarComponents
{
    public partial class TopbarNavItemMessages : ComponentBase
    {
        //[Inject]
        //public HubClientService HubClientService { get; set; }
        private int MessageCount = 0;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //if (!HubClientService.IsConnected)
            //    await HubClientService.InitAsync();
        }
    }
}
