using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_lampshadeQuery.Contracts.Article;
using _01_lampshadeQuery.Contracts.Comment;
using BlogManagement.Infrastructure.EfCore;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_lampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _context = context;
            _commentContext = commentContext;
        }

        public ArticleQueryModel GetArticleBy(string slug)
        {
            var result = _context.Articles
                .Include(x => x.Category)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription
                }).AsNoTracking().FirstOrDefault(x=>x.Slug==slug);

            if (result != null)
            {
                result.KeywordsList = result.Keywords.Split(",").ToList();

                var comments = _commentContext.Comments
                    .Where(x => x.Type == CommentType.Article)
                    .Where(x => !x.Canceled && x.Confirmed)
                    .Select(x => new CommentQueryModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Message = x.Message,
                        CreationDate = x.CreationDate.ToFarsi(),
                        ParentId = x.ParentId

                    }).AsNoTracking().OrderByDescending(x => x.Id).ToList();

                foreach (var comment in comments)
                {
                    if (comment.ParentId > 0)
                    {
                        comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
                    }
                }

                result.Comments = comments;

            }
            return result;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _context.Articles
                .Where(x => !x.IsRemoved)
                .Include(x => x.Category)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryId = x.Category.Id,
                    CategoryName = x.Category.Name,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription
                }).AsNoTracking().Take(6).OrderByDescending(x=>x.Id).ToList();
        }
    }
}
