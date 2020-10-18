using BlazorWorld.Core.Entities.Organization;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Services
{
    public interface IInvitationService
    {
        Task<Invitation[]> GetAllAsync();
        Task<HttpResponseMessage> Invite(Invitation invitation);
    }
}
