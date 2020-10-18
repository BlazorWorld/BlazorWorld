using System.Threading.Tasks;
using BlazorWorld.Core.Entities;
using BlazorWorld.Core.Entities.Organization;

namespace BlazorWorld.Core.Repositories
{
    public interface IInvitationRepository
    {
        Task<int> Add(Invitation invitation);
        Task<string> GetInvitationAsync(string email, string code);
        Task<Invitation[]> GetInvitationsAsync(string createdBy);
    }
}