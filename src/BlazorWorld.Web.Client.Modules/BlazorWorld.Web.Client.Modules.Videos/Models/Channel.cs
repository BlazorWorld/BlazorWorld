using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Helper;

namespace BlazorWorld.Web.Client.Modules.Videos.Models
{
    public class Channel : Node
    {
        public Channel() : base()
        {
            Module = Constants.VideosModule;
            Type = Constants.ChannelType;
            CustomFields = new Core.Entities.Content.NodeCustomFields();
        }

        public static Channel Create(Node node)
        {
            return node.ConvertTo<Channel>();
        }

        public string Name
        {
            get => Title;
            set => Title = value;
        }

        public string Description
        {
            get => Content;
            set => Content = value;
        }

        public string ThumbnailUrl
        {
            get => CustomFields != null ? CustomFields.CustomField1 : string.Empty;
            set => CustomFields.CustomField1 = value;
        }
    }
}
