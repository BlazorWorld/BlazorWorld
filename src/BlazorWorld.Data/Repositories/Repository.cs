using BlazorWorld.Core.Repositories;
using System.Threading.Tasks;

namespace BlazorWorld.Data.Repositories
{
    public class Repository : IRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
