using BlazorWorld.Web.Client.Modules.Videos.Models;
using BlazorWorld.Web.Shared.Services;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Services
{
    public class VideoService : IVideoService
    {
        private readonly IWebNodeService _nodeService;
        public VideoService(IWebNodeService nodeService)
        {
            _nodeService = nodeService;
        }

        public async Task SetVideoAttributesAsync(Video video)
        {
            var result = await GetYouTubeOEmbed(video.Url);
            var jObject = JObject.Parse(result);
            video.EmbedUrl = video.Url.Replace("https://youtu.be/", "https://www.youtube.com/embed/");
            video.Title = (string)jObject["title"];
            video.ThumbnailUrl = (string)jObject["thumbnail_url"];
            video.ThumbnailHeight = (string)jObject["thumbnail_height"];
            video.ThumbnailWidth = (string)jObject["thumbnail_width"];
        }

        private async Task<string> GetYouTubeOEmbed(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                string oEmbedUrl = "https://www.youtube.com/oembed?url=" + url;
                return await _nodeService.SecureGetOEmbed(oEmbedUrl);
            }

            return string.Empty;
        }
    }
}
