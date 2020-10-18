using System.Threading.Tasks;

namespace BlazorWorld.Core.Repositories
{
    public interface IRepository
    {
        Task SaveChangesAsync();
    }
}
