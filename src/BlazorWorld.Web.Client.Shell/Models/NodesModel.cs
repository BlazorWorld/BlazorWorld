using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell
{
    // https://www.mikesdotnetting.com/article/328/simple-paging-in-asp-net-core-razor-pages
    public class NodesModel
    {
        private readonly IWebNodeService _nodeService;

        public NodesModel(IWebNodeService nodeService)
        {
            _nodeService = nodeService;
        }

        public NodeSearch NodeSearch { get; set; }
        public int CurrentPage { get; set; } = 0;
        public int Count { get; set; }
        public int PageSize { get; set; }
        public bool ShowPrevious => CurrentPage > 0;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 0;
        public bool ShowLast => CurrentPage != TotalPages;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public Node[] Data { get; set; }
        public bool IsLoggedIn { get; set; } = false;

        public async Task InitAsync()
        {
            Data = await _nodeService.GetAsync(NodeSearch, CurrentPage);
            Count = await _nodeService.GetCountAsync(NodeSearch);
            PageSize = await _nodeService.GetPageSizeAsync(NodeSearch);
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
            Node[] data;
            if (IsLoggedIn)
                data = await _nodeService.SecureGetAsync(NodeSearch, CurrentPage);
            else
                data = await _nodeService.GetAsync(NodeSearch, CurrentPage);
            AddNodes(data, isNext);
        }

        private void AddNodes(Node[] newNodes, bool isNext)
        {
            var output = new List<Node>();
            if (isNext)
                output = Data.ToList();
            foreach (var item in newNodes)
                output.Add(item);
            if (!isNext)
                output.AddRange(Data.ToList());
            Data = output.ToArray();
        }

        public void Add(Node node, bool addToTop)
        {
            var list = new List<Node>();
            if (addToTop) list.Add(node);
            if (Data != null) list.AddRange(Data);
            if (!addToTop) list.Add(node);
            Data = list.ToArray();
        }

        public void Remove(string id)
        {
            Data = Data.Where(c => c.Id != id).ToArray();
        }
    }
}