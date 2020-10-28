using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Invitations.Pages
{
    public partial class Invitations : ComponentBase
    {
        [Inject]
        protected IWebInvitationService InvitationService { get; set; }
        private Invitation Invitation { get; set; }
        private Invitation[] MyInvitations { get; set; }

        protected async Task SubmitAsync()
        {
            await InvitationService.Invite(Invitation);
        }
    }
}
