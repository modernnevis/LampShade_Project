using _01_lampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponents : ViewComponent
    {


        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
