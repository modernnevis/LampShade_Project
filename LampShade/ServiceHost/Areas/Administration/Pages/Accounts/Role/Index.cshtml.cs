using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<RoleViewModel> Roles;

        private readonly IRoleApplication _roleApplication;

        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            Roles = _roleApplication.List();
        }
        public IActionResult OnGetCreate()
        {

            return Partial("./Create", new CreateRole());
        }

        public JsonResult OnPostCreate(CreateRole command)
        {
            var operationResult = _roleApplication.Create(command);
            return new JsonResult(operationResult);
        }

        public IActionResult OnGetEdit(long id)
        {
            var editRole = _roleApplication.GetDetails(id);
            return Partial("Edit", editRole);
        }

        public JsonResult OnPostEdit(EditRole command)
        {
            var operationResult = _roleApplication.Edit(command);
            return new JsonResult(operationResult);
        }

    }
}
