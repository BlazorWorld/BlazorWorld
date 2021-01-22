using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Helper;

namespace BlazorWorld.Web.Client.Modules.Profiles.Models
{
    public class Profile : Node
    {
        public Profile() : base()
        {
            Module = Constants.ProfilesModule;
            Type = Constants.ProfileType;
            CustomFields = new Core.Entities.Content.NodeCustomFields();
        }

        public static Profile Create(Node node)
        {
            return node.ConvertTo<Profile>();
        }

        public string Headline
        {
            get => CustomFields != null ? CustomFields.CustomField1 : string.Empty;
            set => CustomFields.CustomField1 = value;
        }

        public string About
        {
            get => this.Content;
            set => this.Content = value;
        }
    }
}
