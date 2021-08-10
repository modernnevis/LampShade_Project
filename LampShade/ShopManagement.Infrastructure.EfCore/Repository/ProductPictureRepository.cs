using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long,ProductPicture> , IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Include(x => x.Product)
                .Select(x => new EditProductPicture()
                {
                    Id=x.Id,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ProductId = x.ProductId
                }).SingleOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel search)
        {
            var query = _context.ProductPictures.Include(x => x.Product)
                .Select(x => new ProductPictureViewModel()
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    Product = x.Product.Name,
                    ProductId = x.ProductId,
                    IsRemoved = x.IsRemove
                });
            if (search.ProductId != 0)
                query = query.Where(x => x.ProductId == search.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();

        }
    }
}
