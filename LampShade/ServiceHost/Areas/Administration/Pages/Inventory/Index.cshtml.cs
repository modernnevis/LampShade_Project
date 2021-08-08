using System.Collections.Generic;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Inventory;

        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel( IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory()
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(CreateInventory command)
        {
            var operationResult = _inventoryApplication.Create(command);
            return new JsonResult(operationResult); 
        }

        public IActionResult OnGetEdit(long id)
        {
            var editInventory = _inventoryApplication.GetDetails(id);
            editInventory.Products = _productApplication.GetProducts();
            return Partial("Edit", editInventory);
        }
        public JsonResult OnPostEdit(EditInventory command)
        {
            var operationResult = _inventoryApplication.Edit(command);
            return new JsonResult(operationResult);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory()
            {
                InventoryId = id
            };
            return Partial("./Increase", command);
        }
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var operationResult = _inventoryApplication.Increase(command);
            return new JsonResult(operationResult);
        }

        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory()
            {
                InventoryId = id
            };
            return Partial("./Reduce", command);
        }
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var operationResult = _inventoryApplication.Reduce(command);
            return new JsonResult(operationResult);
        }

        public IActionResult OnGetLog(long id)
        {
            var command = _inventoryApplication.GetOperationLog(id);
            
            return Partial("./OperationLog", command);
        }
    }
}
