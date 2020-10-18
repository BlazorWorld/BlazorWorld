using BlazorWorld.Core.Entities.Content;
using System.Linq;

namespace BlazorWorld.Core.Repositories
{
    public interface IMessageRepository : IRepository
    {
        IQueryable<Message> Get(string groupId);
        void Add(Message message);
    }
}
