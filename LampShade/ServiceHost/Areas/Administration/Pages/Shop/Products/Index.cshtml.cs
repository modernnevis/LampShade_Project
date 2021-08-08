using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public ProductSearchModel SearchModel;
        public List<ProductViewModel> Products;

        public SelectList ProductCategories;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication, IProductApplication productApplication)
        {
            _productCategoryApplication = productCategoryApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            Products = _productApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct()
            {
                Categories = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(CreateProduct command)
        {
            var operationResult = _productApplication.Create(command);
            return new JsonResult(operationResult); 
        }

        public IActionResult OnGetEdit(long id)
        {
            var editProduct = _productApplication.GetDetails(id);
            editProduct.Categories = _productCategoryApplication.GetProductCategories();
            return Partial("Edit", editProduct);
        }
        public JsonResult OnPostEdit(EditProduct command)
        {
            var operationResult = _productApplication.Edit(command);
            return new JsonResult(operationResult);
        }

        //public IActionResult OnGetNotInStock(long id)
        //{
        //    var result = _productApplication.NotInStock(id);
        //    if (result.IsSucceeded)
        //        return RedirectToPage("./Index");

        //    Message = result.Message;
        //    return RedirectToPage("./Index");
        //}
        //public IActionResult OnGetInStock(long id)
        //{
        //    var result = _productApplication.InStock(id);
        //    if (result.IsSucceeded)
        //        return RedirectToPage("./Index");

        //    Message = result.Message;
        //    return RedirectToPage("./Index");
        //}
    }
}
