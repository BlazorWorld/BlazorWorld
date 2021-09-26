using System;
using System.Threading.Tasks;

namespace BlazorWorld.Application.Interfaces.Repositories
{
    public interface INodeRepository
    {
        Task<bool> IsNodeUsed(string nodeId);
    }
}
