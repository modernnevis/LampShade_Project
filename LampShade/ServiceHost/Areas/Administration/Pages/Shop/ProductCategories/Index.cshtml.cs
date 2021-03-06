using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> ProductCategories;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }
        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var operationResult = _productCategoryApplication.Create(command);
            return new JsonResult(operationResult); 
        }

        public IActionResult OnGetEdit(long id)
        {
            var editProductCategory = _productCategoryApplication.GetDetails(id);
            return Partial("Edit", editProductCategory);
        }
        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var operationResult = _productCategoryApplication.Edit(command);
            return new JsonResult(operationResult);
        }
    }
}
