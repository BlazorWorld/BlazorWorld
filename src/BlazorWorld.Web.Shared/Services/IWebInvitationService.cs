using BlazorWorld.Core.Entities.Organization;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebInvitationService
    {
        Task<Invitation[]> GetAllAsync();
        Task<HttpResponseMessage> Invite(Invitation invitation);
    }
}
