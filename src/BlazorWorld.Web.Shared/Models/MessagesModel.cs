using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Web.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Models
{
    public class MessagesModel
    {
        private readonly IWebMessageService _messagesService;

        public MessagesModel(IWebMessageService messagesService)
        {
            _messagesService = messagesService;
        }

        public string Module { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string GroupId { get; set; }
        public int CurrentPage { get; set; } = 0;
        public int Count { get; set; }
        public int PageSize { get; set; }
        public bool ShowPrevious => CurrentPage > 0;
        public bool ShowNext => CurrentPage < (TotalPages - 1);
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != (TotalPages - 1);
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public Message[] Data { get; set; }
        public bool IsLoggedIn { get; set; } = false;

        public async Task InitAsync()
        {
            Count = await _messagesService.GetCountAsync(GroupId);
            PageSize = await _messagesService.GetPageSizeAsync(Module);
            CurrentPage = TotalPages;
            Data = await _messagesService.GetAsync(GroupId, CurrentPage);
        }

        public async Task PreviousAsync()
        {
            CurrentPage--;
            await OnGetAsync(false);
        }

        public async Task NextAsync()
        {
            CurrentPage++;
            await OnGetAsync(true);
        }

        private async Task OnGetAsync(bool isNext)
        {
            Message[] data;
            data = await _messagesService.GetAsync(GroupId, CurrentPage);
            AddMessages(data, isNext);
        }

        private void AddMessages(Message[] newMessages, bool isNext)
        {
            var output = new List<Message>();
            if (isNext)
                output = Data.ToList();
            foreach (var item in newMessages)
                output.Add(item);
            if (!isNext)
                output.AddRange(Data.ToList());
            Data = output.ToArray();
        }

        public void Add(Message message, bool addToTop)
        {
            var list = new List<Message>();
            if (addToTop) list.Add(message);
            if (Data != null) list.AddRange(Data);
            if (!addToTop) list.Add(message);
            Data = list.ToArray();
        }

        public void Remove(string id)
        {
            Data = Data.Where(c => c.Id != id).ToArray();
        }
    }
}