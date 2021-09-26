using BlazorWorld.Application.Features.Nodes.Queries.GetAll;

namespace BlazorWorld.Client.Modules.Articles.Models
{
    public class CategoryResponse : GetAllNodesResponse
    {
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
