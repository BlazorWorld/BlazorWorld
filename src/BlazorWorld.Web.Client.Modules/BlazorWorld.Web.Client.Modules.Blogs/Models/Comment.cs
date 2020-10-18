using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Helper;

namespace BlazorWorld.Web.Client.Modules.Blogs.Models
{
    public class Comment : Node
    {
        public Comment() : base()
        {
            Module = Constants.BlogsModule;
            Type = Constants.CommentType;
        }

        public static Comment Create(Node node)
        {
            return node.ConvertTo<Comment>();
        }
    }
}
