using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Helper;
using System;

namespace BlazorWorld.Web.Client.Modules.Forums.Models
{
    public class Comment : Node
    {
        public Comment() : base()
        {
            Module = Constants.ForumsModule;
            Type = Constants.CommentType;
        }

        public static Comment Create(Node node)
        {
            return node.ConvertTo<Comment>();
        }

        public string PostId
        {
            get => ParentId;
            set => ParentId = value;
        }
    }
}
