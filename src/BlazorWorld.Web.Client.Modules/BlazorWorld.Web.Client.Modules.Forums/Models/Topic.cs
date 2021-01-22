using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Helper;
using System;

namespace BlazorWorld.Web.Client.Modules.Forums.Models
{
    public class Topic : Node
    {        
        public Topic() : base()
        {
            Module = Constants.ForumsModule;
            Type = Constants.TopicType;
            CustomFields = new Core.Entities.Content.NodeCustomFields();
        }

        public static Topic Create(Node node)
        {
            return node.ConvertTo<Topic>();
        }

        public string ForumId
        {
            get => ParentId;
            set => ParentId = value;
        }

        public string Link
        {
            get => CustomFields != null ? CustomFields.CustomField1 : string.Empty;
            set => CustomFields.CustomField1 = value;
        }

        public int PostCount => ChildCount;
    }
}
