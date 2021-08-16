using _01_lampshadeQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArticlesViewComponents : ViewComponent
    {
        private readonly IArticleQuery _articleQuery;

        public LatestArticlesViewComponents(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }


        public IViewComponentResult Invoke()
        {
            var articles = _articleQuery.GetLatestArticles();
            return View(articles);
        }
    }
}
