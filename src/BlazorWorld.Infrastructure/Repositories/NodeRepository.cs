using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorWorld.Application.Interfaces.Repositories;
using BlazorWorld.Domain.Entities.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BlazorWorld.Infrastructure.Repositories
{
    public class NodeRepository : INodeRepository
    {
        private readonly IRepositoryAsync<Node, string> _repository;

        public NodeRepository(IRepositoryAsync<Node, string> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsNodeUsed(string nodeId)
        {
            return await _repository.Entities.AnyAsync(n => n.Id == nodeId);
        }
    }
}