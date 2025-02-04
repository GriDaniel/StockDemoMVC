using Microsoft.AspNetCore.Mvc;

namespace StockDemo.Views.Shared.Components.HeroBoard
{
    public class HeroBoardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
