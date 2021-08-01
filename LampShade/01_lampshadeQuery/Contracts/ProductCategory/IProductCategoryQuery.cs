using System.Collections.Generic;

namespace _01_lampshadeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetList();
    }
}
