using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application.ArticleCategory
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operationResult = new OperationResult();
            if (_articleCategoryRepository.Exists(x => x.Name == command.Name))
                return operationResult.Failed(ApplicationMessages.Duplicate);

            var slug = command.Slug.Slugify();
            var picturePath = $"ArticleCategories/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            var articleCategory = new Domain.ArticleCategoryAgg.ArticleCategory(command.Name, fileName,
                command.PictureAlt, command.PictureTitle, command.Description, command.ShowOrder, slug,
                command.Keywords, command.MetaDescription, command.CanonicalAddress);

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return operationResult.Succeeded();

        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operationResult = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(command.Id);

            if (articleCategory == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            if (_articleCategoryRepository.Exists(x => x.Name == command.Name && x.Id!=command.Id))
                return operationResult.Failed(ApplicationMessages.Duplicate);

            var slug = command.Slug.Slugify();
            var picturePath = $"ArticleCategories/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            articleCategory.Edit(command.Name, fileName,
                command.PictureAlt, command.PictureTitle, command.Description, command.ShowOrder, slug,
                command.Keywords, command.MetaDescription, command.CanonicalAddress);

            _articleCategoryRepository.SaveChanges();
            return operationResult.Succeeded();

        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}
