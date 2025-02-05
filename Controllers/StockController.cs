using Microsoft.AspNetCore.Mvc;
using StockDemo.Models;
using StockDemo.Services;

namespace StockDemo.Controllers
{
    public class StockController : Controller
    {
        private readonly StockInterface _myService;

        public StockController(StockInterface myService)
        {
            _myService = myService;
        }

        public IActionResult Index()
        {
            var stocks = _myService.GetStocks();
            return View(stocks);
        }

        public IActionResult Details(string name)
        {
            var stock = _myService.GetStock(name);
            if (stock == null)
            {
                return Content("Stock not found.");
            }
            return View(stock);
        }

        [HttpPost]
        public IActionResult Create(Stock stock)
        {
            if (stock == null)
            {
                return Content("Error: Invalid stock data.");
            }

            _myService.Create(stock);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Update(string id, Stock stock)
        {
            if (stock == null)
            {
                return Content("Error: Invalid stock data.");
            }

            var existingStock = _myService.GetStock(id);
            if (existingStock == null)
            {
                return Content("Stock not found.");
            }

            _myService.Update(id, stock);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(string name)
        {
            var stock = _myService.GetStock(name);
            if (stock == null)
            {
                return Content("Stock not found.");
            }

            _myService.Delete(name);
            return RedirectToAction(nameof(Index));
        }


    }
}
