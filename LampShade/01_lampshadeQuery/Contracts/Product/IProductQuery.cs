using System.Collections.Generic;

namespace _01_lampshadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetBy(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
