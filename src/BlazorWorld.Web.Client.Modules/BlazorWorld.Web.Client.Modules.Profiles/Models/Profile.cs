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
            CustomFields = new Core.Entities.Common.EntityCustomFields();
        }

        public static Profile Create(Node node)
        {
            return node.ConvertTo<Profile>();
        }

        public string Headline
        {
            get => CustomFields.CustomField1;
            set => CustomFields.CustomField1 = value;
        }

        public string About
        {
            get => this.Content;
            set => this.Content = value;
        }
    }
}
