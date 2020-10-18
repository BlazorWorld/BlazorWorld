using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;

namespace BlazorWorld.Data.Repositories
{
    public class ActivityRepository : Repository, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(Activity activity)
        {
            _dbContext.Activities.Add(activity);
        }
    }
}
