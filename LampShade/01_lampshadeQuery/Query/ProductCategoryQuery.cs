using System.Collections.Generic;
using System.Linq;
using _01_lampshadeQuery.Contracts.ProductCategory;
using ShopManagement.Infrastructure.EfCore;

namespace _01_lampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;

        public ProductCategoryQuery(ShopContext context)
        {
            _context = context;
        }

        public List<ProductCategoryQueryModel> GetList()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel()
            {
                Picture = x.Picture,
                Name = x.Name,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }
    }
}
