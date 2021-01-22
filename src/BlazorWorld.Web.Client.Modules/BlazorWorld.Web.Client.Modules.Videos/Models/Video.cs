using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Helper;

namespace BlazorWorld.Web.Client.Modules.Videos.Models
{
    public class Video : Node
    {
        public Video() : base()
        {
            Module = Constants.VideosModule;
            Type = Constants.VideoType;
            CustomFields = new Core.Entities.Content.NodeCustomFields();
        }

        public static Video Create(Node node)
        {
            return node.ConvertTo<Video>();
        }

        public string ChannelId
        {
            get => ParentId;
            set => ParentId = value;
        }

        public string Url
        {
            get => CustomFields != null ? CustomFields.CustomField1 : string.Empty;
            set => CustomFields.CustomField1 = value;
        }

        public string EmbedUrl
        {
            get => CustomFields != null ? CustomFields.CustomField2 : string.Empty;
            set => CustomFields.CustomField2 = value;
        }

        public string ThumbnailUrl
        {
            get => CustomFields != null ? CustomFields.CustomField3 : string.Empty;
            set => CustomFields.CustomField3 = value;
        }

        public string ThumbnailHeight
        {
            get => CustomFields != null ? CustomFields.CustomField4 : string.Empty;
            set => CustomFields.CustomField4 = value;
        }

        public string ThumbnailWidth
        {
            get => CustomFields != null ? CustomFields.CustomField5 : string.Empty;
            set => CustomFields.CustomField5 = value;
        }
    }
}
