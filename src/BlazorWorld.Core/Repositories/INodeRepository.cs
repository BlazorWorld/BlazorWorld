using BlazorWorld.Core.Entities.Content;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Core.Repositories
{
    public interface INodeRepository : IRepository
    {
        Task<Node> GetAsync(string id);
        IQueryable<Node> Get(NodeSearch search);
        void Add(Node node);
        void Update(Node node);
        void Delete(string id);
        Task<NodeVote> GetVoteAsync(string userId, string nodeId);
        void AddVote(NodeVote vote);
        void RemoveVote(NodeVote vote);
    }
}
