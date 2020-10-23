using BlazorWorld.Web.Client.Modules.Videos.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Services
{
    public class VideoService
    {
        private readonly IHttpClientFactory _clientFactory;
        public VideoService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public void SetVideoAttributes(Video video)
        {
            var result = GetOEmbed(video.Url).Result;
            var jObject = JObject.Parse(result);
            video.EmbedUrl = video.Url.Replace("https://youtu.be/", "https://www.youtube.com/embed/");
            video.Title = (string)jObject["title"];
            video.ThumbnailUrl = (string)jObject["thumbnail_url"];
            video.ThumbnailHeight = (string)jObject["thumbnail_height"];
            video.ThumbnailWidth = (string)jObject["thumbnail_width"];
        }

        private async Task<string> GetOEmbed(string url)
        {
            string oEmbedUrl = "https://youtube.com/oembed?url=" + url;

            if (!string.IsNullOrEmpty(oEmbedUrl))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, oEmbedUrl);

                var client = _clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }

            return string.Empty;
        }
    }
}
