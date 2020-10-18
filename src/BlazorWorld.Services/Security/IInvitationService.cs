using BlazorWorld.Core.Entities.Organization;
using System.Threading.Tasks;

namespace BlazorWorld.Services
{
    public interface IInvitationService
    {
        Task<int> AddAsync(string url, string createdBy, string emails);
        Task<string> GetInvitationAsync(string email, string code);
        Task<Invitation[]> GetInvitationsAsync(string createdBY);
    }
}