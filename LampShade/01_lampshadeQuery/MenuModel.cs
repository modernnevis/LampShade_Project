using System.Collections.Generic;
using _01_lampshadeQuery.Contracts.ArticleCategory;
using _01_lampshadeQuery.Contracts.ProductCategory;

namespace _01_lampshadeQuery
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
