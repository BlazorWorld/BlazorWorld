using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorWorld.Web.Client.Shell;
using Microsoft.AspNetCore.Components;

namespace BlazorWorld.Web.Client.Shell.Components
{
    public partial class Modal : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Text { get; set; }
        [Parameter] 
        public string ConfirmButtonText { get; set; }
        [Parameter] 
        public string CancelButtonText { get; set; }
        [Parameter]
        public EventCallback OnConfirm { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }
        public string ModalDisplay { get; set; } = "none;";
        public string ModalClass { get; set; } = "";
        public bool ShowBackdrop { get; set; } = false;

        public async Task ConfirmAsync()
        {
            await OnConfirm.InvokeAsync(Id);
            Close();
        }

        public async Task CancelAsync()
        {
            await OnCancel.InvokeAsync(Id);
            Close();
        }

        public void Open()
        {
            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            StateHasChanged();
        }

        public void Close()
        {
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            StateHasChanged();
        }
    }
}
