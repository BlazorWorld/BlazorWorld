using BlazorWorld.Application.Features.Nodes.Commands.AddEdit;

namespace BlazorWorld.Client.Modules.Articles.Models
{
    public class AddEditCategoryCommand : AddEditNodeCommand
    {
        public AddEditCategoryCommand() : base()
        {
            Module = Constants.ArticlesModule;
            Type = Constants.CategoryType;
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
