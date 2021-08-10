using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.ProductCategory
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        public OperationResult Create(CreateProductCategory command)
        {
            OperationResult operationResult = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
               return operationResult.Failed(ApplicationMessages.Duplicate);

            var slug = command.Slug.Slugify();
            var picturePath = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            var productCategory = new Domain.ProductCategoryAgg.ProductCategory(command.Name, command.Description,
                fileName, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
                slug);


            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();

        }

        public OperationResult Edit(EditProductCategory command)
        {
            OperationResult operationResult = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if(productCategory==null)
                return operationResult.Failed(ApplicationMessages.NotFound);
            
            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id!=command.Id))
                return operationResult.Failed(ApplicationMessages.Duplicate);

            var slug = command.Slug.Slugify();
            var picturePath = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);
            productCategory.Edit(command.Name, command.Description,
                fileName, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
                slug);

            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}
