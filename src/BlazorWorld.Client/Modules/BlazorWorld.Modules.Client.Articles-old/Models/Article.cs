using BlazorWorld.Application.Features.Nodes.Queries;
using BlazorWorld.Shared;

namespace BlazorWorld.Modules.Client.Articles.Models
{
    public class Article : NodeResponse
    {
        public Article() : base()
        {
            Module = Constants.ArticlesModule;
            Type = Constants.ArticleType;
            CustomFields = new NodeCustomFieldsResponse();
        }

        public static Article Create(NodeResponse node)
        {
            return node.ConvertTo<Article>();
        }

        public string CategoryId
        {
            get => ParentId;
            set => ParentId = value;
        }

        public string Summary
        {
            get => CustomFields != null ? CustomFields.CustomField1 : string.Empty;
            set => CustomFields.CustomField1 = value;
        }
    }
}
