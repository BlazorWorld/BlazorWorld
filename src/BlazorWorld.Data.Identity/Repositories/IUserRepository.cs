using BlazorWorld.Core.Repositories;
using System.Threading.Tasks;

namespace BlazorWorld.Data.Identity.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<ApplicationUser> GetUserAsync(string appUserId);
        Task<ApplicationUser> GetUserByUsernameAsync(string username);
    }
}
