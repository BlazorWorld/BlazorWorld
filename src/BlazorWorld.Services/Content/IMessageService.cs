using BlazorWorld.Core.Entities.Content;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Content
{
    public interface IMessageService
    {
        Task SaveAsync(Message message);
        Task<List<Message>> GetPaginatedResultAsync(string groupId, int currentPage, 
            int pageSize = 10);
        Task<int> GetCountAsync(string groupId);
    }
}
