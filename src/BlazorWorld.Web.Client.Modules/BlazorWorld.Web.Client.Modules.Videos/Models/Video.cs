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
            CustomFields = new Core.Entities.Common.EntityCustomFields();
        }

        public static Video Create(Node node)
        {
            return node.ConvertTo<Video>();
        }

        public string Url
        {
            get => CustomFields.CustomField1;
            set => CustomFields.CustomField1 = value;
        }

        public string EmbedUrl
        {
            get => CustomFields.CustomField2;
            set => CustomFields.CustomField2 = value;
        }

        public string ThumbnailUrl
        {
            get => CustomFields.CustomField3;
            set => CustomFields.CustomField3 = value;
        }

        public string ThumbnailHeight
        {
            get => CustomFields.CustomField4;
            set => CustomFields.CustomField4 = value;
        }

        public string ThumbnailWidth
        {
            get => CustomFields.CustomField4;
            set => CustomFields.CustomField4 = value;
        }
    }
}
