using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorWorld.Core.Entities.Content;

namespace BlazorWorld.Web.Client.Modules.Common.Services
{
    public interface IVoteService
    {
        Task<NodeVote> GetAsync(string id);
        Task<int> AddAsync(string id, bool isUpVote);
    }
}
