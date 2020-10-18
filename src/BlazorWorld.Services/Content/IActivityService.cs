using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorWorld.Core.Entities.Content;

namespace BlazorWorld.Services.Content
{
    public interface IActivityService
    {
        Task<string> AddAsync(string nodeId, string message, string userId);
    }
}
