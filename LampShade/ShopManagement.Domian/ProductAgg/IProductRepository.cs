using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long , Product>
    {
        EditProduct GetDetails(long id);
        Product GetProductWithCategoryBy(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();

    }
}
