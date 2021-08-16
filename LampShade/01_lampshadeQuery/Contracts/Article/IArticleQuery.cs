using System;
using System.Collections.Generic;
using System.Text;
using BlogManagement.Application.Contracts.Article;

namespace _01_lampshadeQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        ArticleQueryModel GetArticleBy(string slug);
        List<ArticleQueryModel> GetLatestArticles();
    }
}
