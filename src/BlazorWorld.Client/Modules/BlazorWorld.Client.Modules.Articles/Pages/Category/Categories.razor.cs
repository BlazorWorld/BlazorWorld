using BlazorHero.CleanArchitecture.Client.Infrastructure.Extensions;
using BlazorWorld.Application.Requests.Content;
using BlazorWorld.Client.Modules.Articles.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWorld.Client.Modules.Articles.Pages.Category
{
    public partial class Categories
    {
        public List<CategoryResponse> NodeList = new List<CategoryResponse>();
        private string searchString = "";
        [CascadingParameter] public HubConnection hubConnection { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCategoriesAsync();
            hubConnection = hubConnection.TryInitialize(_navigationManager);
            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await hubConnection.StartAsync();
            }
        }

        private async Task GetCategoriesAsync()
        {
            var request = new GetAllPagedNodesRequest()
            {

            };
            var response = await _nodeManager.GetNodesAsync(request);
            if (response.Succeeded)
            {
                //NodeList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(localizer[message], Severity.Error);
                }
            }
        }
    }
}
