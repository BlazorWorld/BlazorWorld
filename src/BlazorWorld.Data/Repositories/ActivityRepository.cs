using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Data.DbContexts;

namespace BlazorWorld.Data.Repositories
{
    public class ActivityRepository : Repository, IActivityRepository
    {
        public ActivityRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(Activity activity)
        {
            _dbContext.Activities.Add(activity);
        }
    }
}
