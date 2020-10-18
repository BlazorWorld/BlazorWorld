using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Data.Identity;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Content
{
    public interface IProfileService
    {
        Task Add(ApplicationUser user);
        Task Add(Node profile);
        Task Update(Node profile);
    }
}
