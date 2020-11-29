using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Content
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(
            IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task SaveAsync(Message message)
        {
            message.Id = Guid.NewGuid().ToString();
            _messageRepository.Add(message);
            await _messageRepository.SaveChangesAsync();
        }

        // https://www.mikesdotnetting.com/article/328/simple-paging-in-asp-net-core-razor-pages
        public async Task<List<Message>> GetPaginatedResultAsync(string groupId, int currentPage, int pageSize = 10)
        {
            var data = Get(groupId);
            return await data.Skip(currentPage * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetCountAsync(string groupId)
        {
            var data = Get(groupId);
            return await data.CountAsync();
        }

        public IQueryable<Message> Get(string groupId)
        {
            var messages = _messageRepository.Get(groupId);
            var sortedMessages = messages.OrderBy(m => m.CreatedDate);
            return sortedMessages;
        }
    }
}
