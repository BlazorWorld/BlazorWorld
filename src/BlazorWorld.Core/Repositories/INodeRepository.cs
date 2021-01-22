using BlazorWorld.Core.Entities.Content;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlazorWorld.Core.Repositories
{
    public interface INodeRepository : IRepository
    {
        Task<Node> GetAsync(Expression<Func<Node, bool>> predicate);
        IQueryable<Node> Get(NodeSearch search);
        Task AddAsync(Node node);
        Task UpdateAsync(Node node);
        void Delete(string id);
        Task<NodeVote> GetVoteAsync(string userId, string nodeId);
        void AddVote(NodeVote vote);
        void RemoveVote(NodeVote vote);
    }
}
