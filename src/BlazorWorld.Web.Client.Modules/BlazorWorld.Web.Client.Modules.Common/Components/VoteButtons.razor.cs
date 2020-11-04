using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWorld.Web.Client.Modules.Common.Components
{
    public partial class VoteButtons : ComponentBase
    {
        [Inject]
        public IWebVoteService VoteService { get; set; }
        [Parameter]
        public string NodeId { get; set; }
        [Parameter]
        public bool CanVote { get; set; }
        [Parameter]
        public int? Votes { get; set; }
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private NodeVote Vote { get; set; }
        private bool HasUpVote { get; set; } = false;
        private bool HasDownVote { get; set; } = false;

        protected override async Task OnParametersSetAsync()
        {
            if ((await AuthenticationStateTask).IsAuthenticated())
                await UpdateVoteAsync();
        }

        private async Task UpdateVoteAsync()
        {
            if (!string.IsNullOrEmpty(NodeId))
            {
                Vote = await VoteService.GetAsync(NodeId);
                if (Vote != null)
                {
                    HasUpVote = Vote.Score > 0;
                    HasDownVote = Vote.Score < 0;
                }
                else
                {
                    HasUpVote = false;
                    HasDownVote = false;
                }
            }
        }

        public async Task UpVoteAsync()
        {
            Votes = await VoteService.AddAsync(NodeId, true);
            await UpdateVoteAsync();
        }

        public async Task DownVoteAsync()
        {
            Votes = await VoteService.AddAsync(NodeId, false);
            await UpdateVoteAsync();
        }
    }
}
