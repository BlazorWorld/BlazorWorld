using BlazorWorld.Core.Repositories;
using BlazorWorld.Data.DbContexts;
using System.Threading.Tasks;

namespace BlazorWorld.Data.Repositories
{
    public class Repository : IRepository
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
