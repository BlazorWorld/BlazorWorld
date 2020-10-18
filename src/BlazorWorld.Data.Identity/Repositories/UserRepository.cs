using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Data.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationIdentityDbContext _dbContext;

        public UserRepository(ApplicationIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser> GetUserAsync(string appUserId)
        {
            var user = await _dbContext.Users.FindAsync(appUserId);

            return user;
        }

        public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            var user = await (from au in _dbContext.Users
                        where au.UserName == username
                        select au).SingleOrDefaultAsync();

            return user;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
