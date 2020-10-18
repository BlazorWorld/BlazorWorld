using BlazorWorld.Core.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Organization
{
    public interface IGroupService
    {
        Task<Group> GetAsync(string id);
        Task<GroupMember[]> GetGroupMembersAsync(string groupId);
        Task<Group[]> GetAllAsync(string userId, string module);
        Task<Group> GetByKeyAsync(string key);
        Task AddAsync(Group group);
        Task UpdateAsync(Group group);
    }
}
