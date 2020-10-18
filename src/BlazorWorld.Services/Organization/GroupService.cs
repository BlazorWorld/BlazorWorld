using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Organization
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationDbContext _dbContext;

        public GroupService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Group> GetAsync(string id)
        {
            return await _dbContext.Groups.FindAsync(id);
        }

        public async Task<Group[]> GetAllAsync(string userId, string module)
        {
            var groups = (from g in _dbContext.Groups
                         join gm in _dbContext.GroupMembers on g.Id equals gm.GroupId
                         where
                            (string.IsNullOrEmpty(module) || g.Module == module) && 
                            gm.UserId == userId
                         select g).Distinct();
            return await groups.ToArrayAsync();
        }

        public async Task<GroupMember[]> GetGroupMembersAsync(string groupId)
        {
            var groupMembers = from gm in _dbContext.GroupMembers
                               where gm.GroupId == groupId
                               select gm;
            return await groupMembers.ToArrayAsync();
        }

        public async Task<Group> GetByKeyAsync(string key)
        {
            return await (from g in _dbContext.Groups
                          where g.Key == key
                          select g).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Group group)
        {
            group.Id = Guid.NewGuid().ToString();
            group.CreatedDate = DateTimeOffset.UtcNow.ToString("s");
            group.SetKeyFromMemberIds();
            _dbContext.Groups.Add(group);

            foreach (var userId in group.MemberIds)
            {
                if (userId != group.CreatedBy)
                {
                    var groupMember = new GroupMember()
                    {
                        Id = Guid.NewGuid().ToString(),
                        GroupId = group.Id,
                        UserId = userId,
                        CreatedDate = group.CreatedDate,
                        Role = Roles.Member
                    };
                    _dbContext.GroupMembers.Add(groupMember);
                }
            }

            _dbContext.GroupMembers.Add(new GroupMember()
            {
                Id = Guid.NewGuid().ToString(),
                GroupId = group.Id,
                UserId = group.CreatedBy,
                CreatedDate = group.CreatedDate,
                Role = Roles.Admin
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Group group)
        {
            group.LastUpdatedDate = DateTimeOffset.UtcNow.ToString("s");
            _dbContext.Groups.Update(group);
            await _dbContext.SaveChangesAsync();
        }
    }
}
