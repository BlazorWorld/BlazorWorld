using BlazorWorld.Core.Entities.Organization;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebGroupService
    {
        Task<Group> SecureGetAsync(string id);
        Task<Group[]> SecureGetAllAsync(string module);
        Task<Group> SecureGetByKeyAsync(string key);
        Task<GroupMember[]> SecureGetGroupMembersAsync(string groupId);
        Task<HttpResponseMessage> PostAsync(Group group);
        Task<HttpResponseMessage> PutAsync(Group group);
    }
}
