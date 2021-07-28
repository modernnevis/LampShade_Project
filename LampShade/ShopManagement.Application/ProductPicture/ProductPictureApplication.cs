using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.ProductPicture
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operationResult = new OperationResult();
            if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
                return operationResult.Failed(ApplicationMessages.Duplicate);
            var productPicture = new Domain.ProductPictureAgg.ProductPicture(command.ProductId, command.Picture,
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

            if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessages.Duplicate);

            productPicture.Edit(command.ProductId, command.Picture,
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
