using BlazorWorld.Web.Client.Common;
using BlazorWorld.Web.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Services
{
    public class ConfigurationService : ApiService, IConfigurationService
    {
        private const string API_URL = "api/Configuration";
        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public ConfigurationService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<SidebarMenuSetting[]> SidebarMenuSettingsAsync()
        {
            var request = $"{API_URL}/SidebarMenuSettings";
            return await PublicHttpClient.GetFromJsonAsync<SidebarMenuSetting[]>(request);
        }
    }
}
