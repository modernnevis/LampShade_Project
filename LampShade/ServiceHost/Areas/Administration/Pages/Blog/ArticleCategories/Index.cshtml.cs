using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    public class IndexModel : PageModel
    {
        public ArticleCategorySearchModel SearchModel;
        public List<ArticleCategoryViewModel> ArticleCategories;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }


        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }
        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var operationResult = _articleCategoryApplication.Create(command);
            return new JsonResult(operationResult); 
        }

        public IActionResult OnGetEdit(long id)
        {
            var editArticleCategory = _articleCategoryApplication.GetDetails(id);
            return Partial("Edit", editArticleCategory);
        }
        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var operationResult = _articleCategoryApplication.Edit(command);
            return new JsonResult(operationResult);
        }
    }
}
