using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_lampshadeQuery.Contracts.Product;
using _01_lampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value { get; set; }
        public List<ProductQueryModel> Products;
        private readonly IProductQuery _productQuery;

        public SearchModel( IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string value)
        {
            Value = value;
            Products = _productQuery.Search(value);
        }
    }
}
