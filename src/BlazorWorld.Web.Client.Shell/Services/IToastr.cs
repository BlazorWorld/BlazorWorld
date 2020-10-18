using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Services
{
    public interface IToastr
    {
        Task InfoAsync(string message);
        Task SuccessAsync(string message);
        Task WarningAsync(string message);
        Task ErrorAsync(string message);
        Task RemoveAsync();
        Task ClearAsync();
    }
}
