using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Data.Repositories
{
    public class MessageRepository : Repository, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Message> Get(string groupId)
        {
            return from m in _dbContext.Messages
                   where m.GroupId == groupId
                   select m;
        }

        public void Add(Message message)
        {
            _dbContext.Messages.Add(message);
        }
    }
}
