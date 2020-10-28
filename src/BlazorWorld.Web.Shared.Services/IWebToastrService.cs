using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebToastrService
    {
        Task InfoAsync(string message);
        Task SuccessAsync(string message);
        Task WarningAsync(string message);
        Task ErrorAsync(string message);
        Task RemoveAsync();
        Task ClearAsync();
    }
}
