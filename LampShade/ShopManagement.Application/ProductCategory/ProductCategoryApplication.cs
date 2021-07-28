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
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
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
            var productCategory = new Domain.ProductCategoryAgg.ProductCategory(command.Name, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
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
            productCategory.Edit(command.Name, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
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
