using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public List<SlideViewModel> Slides;

        private readonly ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet()
        {
            Slides = _slideApplication.GetList();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateSlide());
        }
        public JsonResult OnPostCreate(CreateSlide command)
        {
            var operationResult = _slideApplication.Create(command);
            return new JsonResult(operationResult); 
        }

        public IActionResult OnGetEdit(long id)
        {
            var editSlide = _slideApplication.GetDetails(id);
            return Partial("Edit", editSlide);
        }
        public JsonResult OnPostEdit(EditSlide command)
        {
            var operationResult = _slideApplication.Edit(command);
            return new JsonResult(operationResult);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _slideApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
