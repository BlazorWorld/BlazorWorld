using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Content
{
    public interface INodeService
    {
        Task<Node> GetAsync(string id);
        Task<List<Node>> GetPaginatedResultAsync(NodeSearch nodeSearch, int currentPage, int pageSize = 10);
        Task<int> GetCountAsync(NodeSearch nodeSearch);
        Task<string> AddAsync(Node node);
        Task UpdateAsync(Node node);
        Task DeleteAsync(string id);
        Task<NodeVote> GetVoteAsync(string userId, string nodeId);
        Task<int> VoteAsync(string userId, string nodeId, bool isUpVote);
    }
}
