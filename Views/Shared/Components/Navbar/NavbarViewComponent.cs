using Microsoft.AspNetCore.Mvc;

namespace StockDemo.Views.Shared.Components.Navbar
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
