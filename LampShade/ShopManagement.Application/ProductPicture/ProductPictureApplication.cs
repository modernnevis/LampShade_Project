using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.ProductPicture
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader,
            IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operationResult = new OperationResult();
            //if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
            //    return operationResult.Failed(ApplicationMessages.Duplicate);

            var product = _productRepository.GetProductWithCategoryBy(command.ProductId);
            var picturePath = $"{product.Category.Slug}/{product.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            var productPicture = new Domain.ProductPictureAgg.ProductPicture(command.ProductId, fileName,
                command.PictureTitle, command.PictureAlt);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operationResult = new OperationResult();
            var productPicture = _productPictureRepository.Get(command.Id);

            if (productPicture == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            //if (_productPictureRepository.Exists(x =>
            //    x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id != command.Id))
            //    return operationResult.Failed(ApplicationMessages.Duplicate);
            var product = _productRepository.GetProductWithCategoryBy(command.ProductId);
            var picturePath = $"{product.Category.Slug}/{product.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            productPicture.Edit(command.ProductId, fileName,
                command.PictureTitle, command.PictureAlt);
            _productPictureRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);

            if (productPicture == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);

            if (productPicture == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}