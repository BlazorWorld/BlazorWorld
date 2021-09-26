using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Core.Helper;

namespace BlazorWorld.Web.Client.Modules.Messages.Models
{
    public class MessagesGroup : Group
    {
        public MessagesGroup() : base()
        {
            Module = Constants.MessagesModule;
        }

        public static MessagesGroup Create(Group group)
        {
            return group.ConvertTo<MessagesGroup>();
        }
    }
}
