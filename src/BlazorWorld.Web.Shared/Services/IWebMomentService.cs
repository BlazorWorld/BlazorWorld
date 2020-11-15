using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebMomentService
    {
        Task<string> FromNowAsync(string date);
        Task<string> LocalDateAsync(string date, string format);
    }
}
