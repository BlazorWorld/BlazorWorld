using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Invitations.Pages
{
    public partial class Invitations : ComponentBase
    {
        [Inject]
        protected IInvitationService InvitationService { get; set; }
        private Invitation Invitation { get; set; }
        private Invitation[] MyInvitations { get; set; }

        protected async Task SubmitAsync()
        {
            await InvitationService.Invite(Invitation);
        }
    }
}
