using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Comments
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public SelectList Products;
        public CommentSearchModel SearchModel;
        public List<CommentViewModel> Comments;
        private readonly ICommentApplication _commentApplication;
        private readonly IProductApplication _productApplication;


        public IndexModel(ICommentApplication commentApplication, IProductApplication productApplication)
        {
            _commentApplication = commentApplication;
            _productApplication = productApplication;
        }


        public void OnGet(CommentSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Comments = _commentApplication.Search(searchModel);
        }

        public IActionResult OnGetCancel(long id , string productSlug)
        {
            var result = _commentApplication.Cancel(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index" );

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetConfirm(long id)
        {
            var result = _commentApplication.Confirm(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
