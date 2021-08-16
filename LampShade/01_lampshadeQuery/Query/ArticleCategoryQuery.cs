using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_lampshadeQuery.Contracts.Article;
using _01_lampshadeQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_lampshadeQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public ArticleCategoryQueryModel GetWithArticles(string slug)
        {
            var result = _context.ArticleCategories.Select(x => new ArticleCategoryQueryModel
            {
                Slug = x.Slug,
                ArticleCount = x.Articles.Count,
                CanonicalAddress = x.CanonicalAddress,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Articles = MapArticles(x.Articles)
            }).AsNoTracking().FirstOrDefault(x=>x.Slug==slug);

            if (result != null)
            {
                result.KeywordsList = result.Keywords.Split(",").ToList();
                
            }
            return result;
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel
            {
                Id = x.Id, 
                Title = x.Title,
                CanonicalAddress = x.CanonicalAddress,
                Slug = x.Slug,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                PublishDate = x.PublishDate.ToFarsi()

            }).OrderByDescending(x=>x.Id).ToList();
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _context.ArticleCategories.Select(article => new ArticleCategoryQueryModel
            {
                Name = article.Name,
                ArticleCount = article.Articles.Count,
                //Picture = article.Picture,
                //PictureAlt = article.PictureAlt,
                //PictureTitle = article.PictureTitle,
                Slug = article.Slug
            }).AsNoTracking().ToList();
        }
    }
}
