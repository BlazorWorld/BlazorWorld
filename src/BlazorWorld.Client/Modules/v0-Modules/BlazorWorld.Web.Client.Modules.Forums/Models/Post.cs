using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Helper;
using System;

namespace BlazorWorld.Web.Client.Modules.Forums.Models
{
    public class Post : Node
    {
        public Post() : base()
        {
            Module = Constants.ForumsModule;
            Type = Constants.PostType;
        }

        public static Post Create(Node node)
        {
            return node.ConvertTo<Post>();
        }

        public string TopicId
        {
            get => ParentId;
            set => ParentId = value;
        }

        public int CommentCount => ChildCount;
    }
}
