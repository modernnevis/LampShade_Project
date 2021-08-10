using System;
using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.Product
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.Duplicate);

            var slug = command.Slug.Slugify();

            var slugCategory = _productCategoryRepository.Get(command.CategoryId).Slug;
            var picturePath = $"{slugCategory}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            var product = new Domain.ProductAgg.Product(command.Name, command.Code,
                command.ShortDescription, command.Description, fileName, command.PictureAlt,
                command.PictureTitle, command.CategoryId, slug, command.Keywords, command.MetaDescription);

            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategoryBy(command.Id);
            if (product == null)
                return operation.Failed(ApplicationMessages.NotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.Duplicate);

            var slug = command.Slug.Slugify();

            var picturePath = $"{product.Category.Slug}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            product.Edit(command.Name, command.Code,
                command.ShortDescription, command.Description, fileName, command.PictureAlt,
                command.PictureTitle, command.CategoryId, slug, command.Keywords, command.MetaDescription);

            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        //public OperationResult InStock(long id)
        //{
        //    var operation = new OperationResult();
        //    var product = _productRepository.Get(id);
        //    if (product == null)
        //        return operation.Failed(ApplicationMessages.NotFound);

        //    product.InStock();
        //    _productRepository.SaveChanges();
        //    return operation.Succeeded();
        //}

        //public OperationResult NotInStock(long id)
        //{
        //    var operation = new OperationResult();
        //    var product = _productRepository.Get(id);
        //    if (product == null)
        //        return operation.Failed(ApplicationMessages.NotFound);

        //    product.NotInStock();
        //    _productRepository.SaveChanges();
        //    return operation.Succeeded();
        //}
    }
}
