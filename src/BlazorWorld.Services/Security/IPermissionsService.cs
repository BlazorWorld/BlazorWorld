using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Services.Configuration.Models;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Security
{
    public interface IPermissionsService
    {
        Task<bool> AllowedAsync(
            string module,
            string type,
            string action,
            string role,
            bool useCache);
        Task AddAsync(Permission permission);
    }
}
