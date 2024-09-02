using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;
using System.Diagnostics;

namespace MyApp.Controllers
{
    [Authorize] // Ensure only authenticated users can access this controller
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyAppContext _context;

        public HomeController(ILogger<HomeController> logger, MyAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Product.ToListAsync();
            var stockmovements = await _context.StockMovement.ToListAsync();

            var totalStockOnHand = products.Sum(p => p.StockQuantity);
            var totalTransactions = stockmovements.Count();
            var runningOutOfStockCount = products.Count(p => p.StockQuantity == 0);

            ViewBag.TotalStockOnHand = totalStockOnHand;
            ViewBag.TotalTransactions = totalTransactions;
            ViewBag.RunningOutOfStockCount = runningOutOfStockCount;

            var today = DateTime.UtcNow;
            var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfPreviousMonth = firstDayOfCurrentMonth.AddDays(-1);
            var firstDayOfPreviousMonth = new DateTime(lastDayOfPreviousMonth.Year, lastDayOfPreviousMonth.Month, 1);

            // Compute beginning and ending inventory for each category
            var beginningInventory = products
                .Where(p => p.DateAdded < firstDayOfPreviousMonth)
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    BeginningInventory = g.Sum(p => p.StockQuantity)
                })
                .ToList();

            var endingInventory = products
                .Where(p => p.DateAdded <= lastDayOfPreviousMonth)
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    EndingInventory = g.Sum(p => p.StockQuantity)
                })
                .ToList();

            // Join beginning and ending inventory data
            var averageByCategory = beginningInventory
                .Join(endingInventory,
                      beg => beg.Category,
                      end => end.Category,
                      (beg, end) => new
                      {
                          Category = beg.Category,
                          AverageStock = (beg.BeginningInventory + end.EndingInventory) / 2.0
                      })
                .ToList();

            ViewBag.AverageByCategory = averageByCategory;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
