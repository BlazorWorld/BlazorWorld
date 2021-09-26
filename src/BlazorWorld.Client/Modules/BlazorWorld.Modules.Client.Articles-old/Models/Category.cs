﻿using BlazorWorld.Domain.Entities.Node;
using BlazorWorld.Core.Helper;

namespace BlazorWorld.Modules.Client.Articles.Models
{
    public class Category : Node
    {
        public Category() : base()
        {
            Module = Constants.ArticlesModule;
            Type = Constants.CategoryType;
        }

        public static Category Create(Node node)
        {
            return node.ConvertTo<Category>();
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
