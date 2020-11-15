using BlazorWorld.Core.Entities.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebMessageService
    {
        Task<Message[]> GetAsync(string groupId, int currentPage);
        Task<int> GetCountAsync(string groupId);
        Task<int> GetPageSizeAsync(string module);
    }
}
