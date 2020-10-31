using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Helper;

namespace BlazorWorld.Web.Client.Modules.Articles.Models
{
    public class Article : Node
    {
        public Article() : base()
        {
            Module = Constants.ArticlesModule;
            Type = Constants.ArticleType;
            CustomFields = new Core.Entities.Common.EntityCustomFields();
        }

        public static Article Create(Node node)
        {
            return node.ConvertTo<Article>();
        }

        public string Summary
        {
            get => CustomFields != null ? CustomFields.CustomField1 : string.Empty;
            set => CustomFields.CustomField1 = value;
        }
    }
}
