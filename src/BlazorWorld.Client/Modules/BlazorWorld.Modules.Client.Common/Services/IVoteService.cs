using BlazorWorld.Domain.Entities.Node;
using System.Threading.Tasks;

namespace BlazorWorld.Modules.Client.Common.Services
{
    public interface IVoteService
    {
        Task<NodeVote> GetAsync(string id);
        Task<int> AddAsync(string id, bool isUpVote);
    }
}
