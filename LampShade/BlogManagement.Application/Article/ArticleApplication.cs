using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application.Article
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader, IArticleRepository articleRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
            _articleRepository = articleRepository;
        }


        public OperationResult Create(CreateArticle command)
        {
            var operationResult = new OperationResult();
            if (_articleRepository.Exists(x => x.Title == command.Title))
                return operationResult.Failed(ApplicationMessages.Duplicate);

            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var slug = command.Slug.Slugify();
            var picturePath = $"ArticleCategories/{_articleCategoryRepository.GetSlugBy(command.CategoryId)}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            var article = new Domain.ArticleAgg.Article(command.Title, command.ShortDescription, command.Description,
                fileName, command.PictureAlt, command.PictureTitle, publishDate, slug,
                command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);

            _articleRepository.Create(article);
            _articleRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operationResult = new OperationResult();
            var article = _articleRepository.Get(command.Id);

            if (article == null)
                return operationResult.Failed(ApplicationMessages.NotFound);
            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id!=command.Id))
                return operationResult.Failed(ApplicationMessages.Duplicate);

            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var slug = command.Slug.Slugify();
            var picturePath = $"ArticleCategories/{_articleCategoryRepository.GetSlugBy(command.CategoryId)}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            article.Edit(command.Title, command.ShortDescription, command.Description,
                fileName, command.PictureAlt, command.PictureTitle, publishDate, slug,
                command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);

            _articleRepository.SaveChanges();
            return operationResult.Succeeded();

        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var article = _articleRepository.Get(id);

            if (article == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            article.Remove();
            _articleRepository.SaveChanges();
            return operationResult.Succeeded();

        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var article = _articleRepository.Get(id);

            if (article == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            article.Restore();
            _articleRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
