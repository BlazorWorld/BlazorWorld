using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Helper;

namespace BlazorWorld.Web.Client.Modules.Blogs.Models
{
    public class Blog : Node
    {
        public Blog() : base()
        {
            Module = Constants.BlogsModule;
            Type = Constants.BlogType;
        }

        public static Blog Create(Node node)
        {
            return node.ConvertTo<Blog>();
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
    }
}
