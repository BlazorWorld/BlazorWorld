using BlazorWorld.Core.Entities.Content;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebVoteService
    {
        Task<NodeVote> GetAsync(string id);
        Task<int> AddAsync(string id, bool isUpVote);
    }
}
