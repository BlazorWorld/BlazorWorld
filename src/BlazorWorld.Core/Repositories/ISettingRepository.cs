using BlazorWorld.Core.Entities.Configuration;
using System.Threading.Tasks;

namespace BlazorWorld.Core.Repositories
{
    public interface ISettingRepository : IRepository
    {
        Task<Setting> GetAsync(string id);
        Task<Setting[]> GetAllByTypeAsync(string type);
        void Add(Setting setting);
        void Update(Setting setting);
        void Delete(string id);
    }
}
