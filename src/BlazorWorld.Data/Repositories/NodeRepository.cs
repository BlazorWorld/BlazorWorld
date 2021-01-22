using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlazorWorld.Data.Repositories
{
    public class NodeRepository : Repository, INodeRepository
    {
        public NodeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Node> GetAsync(Expression<Func<Node, bool>> predicate)
        {
            var node = await _dbContext.Nodes.SingleOrDefaultAsync(predicate);
            if (node != null) await SetLinksAsync(node);
            return node;
        }

        public IQueryable<Node> Get(NodeSearch search)
        {
            var nodes = from n in _dbContext.Nodes
                            .Include(n => n.CustomFields)
                        where (
                            (string.IsNullOrEmpty(search.Module) || n.Module == search.Module) &&
                            (string.IsNullOrEmpty(search.Type) || n.Type == search.Type) &&
                            (string.IsNullOrEmpty(search.Slug) || n.Slug == search.Slug) &&
                            (string.IsNullOrEmpty(search.ParentId) || n.ParentId == search.ParentId) &&
                            (!search.RootOnly || string.IsNullOrEmpty(n.ParentId)) &&
                            (string.IsNullOrEmpty(search.GroupId) || n.GroupId == search.GroupId)
                        )
                        select n;

            var orderByItems = search.OrderByItems();
            if (orderByItems.Length > 0)
            {
                IOrderedQueryable<Core.Entities.Content.Node> sortedNodes = null;
                switch (orderByItems[0])
                {
                    case (OrderBy.Title):
                        sortedNodes = nodes.OrderBy(i => i.Title);
                        break;
                    case (OrderBy.Weight):
                        sortedNodes = nodes.OrderByDescending(i => i.Weight);
                        break;
                    case (OrderBy.Earliest):
                        sortedNodes = nodes.OrderBy(i => i.CreatedDate);
                        break;
                    case (OrderBy.Latest):
                        sortedNodes = nodes.OrderByDescending(i => i.CreatedDate);
                        break;
                    case (OrderBy.ChildCount):
                        sortedNodes = nodes.OrderByDescending(i => i.ChildCount);
                        break;
                    case (OrderBy.DescendantCount):
                        sortedNodes = nodes.OrderByDescending(i => i.DescendantCount);
                        break;
                    case (OrderBy.Hot):
                        sortedNodes = nodes.OrderByDescending(i => i.Hot);
                        break;
                }

                if (orderByItems.Length > 1)
                {
                    for (int i = 1; i < orderByItems.Length; i++)
                    {
                        switch (orderByItems[i])
                        {
                            case (OrderBy.Title):
                                sortedNodes = sortedNodes.ThenBy(i => i.Title);
                                break;
                            case (OrderBy.Weight):
                                sortedNodes = sortedNodes.ThenByDescending(i => i.Weight);
                                break;
                            case (OrderBy.Earliest):
                                sortedNodes = sortedNodes.ThenBy(i => i.CreatedDate);
                                break;
                            case (OrderBy.Latest):
                                sortedNodes = sortedNodes.ThenByDescending(i => i.CreatedDate);
                                break;
                            case (OrderBy.ChildCount):
                                sortedNodes = sortedNodes.ThenByDescending(i => i.ChildCount);
                                break;
                            case (OrderBy.DescendantCount):
                                sortedNodes = sortedNodes.ThenByDescending(i => i.DescendantCount);
                                break;
                            case (OrderBy.Hot):
                                sortedNodes = sortedNodes.ThenByDescending(i => i.Hot);
                                break;
                        }
                    }

                    return sortedNodes;
                }
            }

            return nodes;
        }

        public async Task AddAsync(Node node)
        {
            await AddOrUpdateLinksAsync(node);
            _dbContext.Nodes.Add(node);
        }

        public async Task UpdateAsync(Node node)
        {
            await AddOrUpdateLinksAsync(node);
            _dbContext.Nodes.Update(node);
        }

        public void Delete(string id)
        {
            _dbContext.Remove(_dbContext.Nodes.Single(i => i.Id == id));
        }

        #region Links

        private async Task SetLinksAsync(Node node)
        {
            var links = await _dbContext.NodeLinks.Where(nl => nl.FromNodeId == node.Id).ToListAsync();
            var linkDictionary = new Dictionary<string, string>();
            foreach (var link in links)
            {
                if (!linkDictionary.ContainsKey(link.Type))
                    linkDictionary.Add(link.Type, string.Empty);
                if (!string.IsNullOrEmpty(linkDictionary[link.Type]))
                    linkDictionary[link.Type] += ',';
                linkDictionary[link.Type] += link.ToNodeId;
            }

            node.Links = string.Join(";", linkDictionary.Select(l => $"{l.Key}:{l.Value}"));
        }

        private async Task ClearLinksAsync(string id)
        {
            var links = await _dbContext.NodeLinks.Where(nl => nl.FromNodeId == id).ToListAsync();
            foreach (var link in links)
            {
                _dbContext.NodeLinks.Remove(link);
            }
        }

        private async Task AddOrUpdateLinksAsync(Node node)
        {
            await ClearLinksAsync(node.Id);
            var linkSets = node.Links.Split(';');
            foreach (var linkSet in linkSets)
            {
                var linkSetFields = linkSet.Split(':');
                var type = linkSetFields[0].Trim();
                var links = linkSetFields[1].Split(',');
                foreach (var linkedNodeId in links)
                {
                    AddLink(type, node.Id, linkedNodeId.Trim());
                }
            }
        }

        private void AddLink(string type, string fromId, string toId)
        {
            var nodeLink = new NodeLink()
            {
                Id = Guid.NewGuid().ToString(),
                Type = type,
                FromNodeId = fromId,
                ToNodeId = toId
            };
            _dbContext.NodeLinks.Add(nodeLink);
        }

        #endregion

        #region Votes

        public async Task<NodeVote> GetVoteAsync(string userId, string nodeId)
        {
            var vote = from v in _dbContext.NodeVotes
                       where v.NodeId == nodeId && v.UserId == userId
                       select v;
            return await vote.FirstOrDefaultAsync();
        }

        public void AddVote(NodeVote vote)
        {
            _dbContext.NodeVotes.Add(vote);
        }

        public void RemoveVote(NodeVote vote)
        {
            _dbContext.NodeVotes.Remove(vote);
        }

        #endregion
    }
}
