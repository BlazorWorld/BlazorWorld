using System.Threading.Tasks;

namespace BlazorWorld.Modules.Client.Common.Services
{
    public interface IMomentService
    {
        Task<string> FromNowAsync(string date);
        Task<string> LocalDateAsync(string date, string format);
    }
}
