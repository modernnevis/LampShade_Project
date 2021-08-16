using System;
using System.Collections.Generic;
using System.Text;

namespace _01_lampshadeQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetWithArticles(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}
