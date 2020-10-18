using BlazorWorld.Core.Entities.Content;

namespace BlazorWorld.Core.Repositories
{
    public interface IActivityRepository : IRepository
    {
        void Add(Activity activity);
    }
}
