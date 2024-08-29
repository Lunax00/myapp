using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;
using System.Diagnostics;
using WebApplication1.Models;

namespace MyApp.Controllers
{
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
            // Fetch products from the database
            var products = await _context.Product.ToListAsync();

            // Calculate average stock quantity by category
            var averageByCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    AverageStock = g.Average(p => p.StockQuantity)
                })
                .ToList();

            // Pass the data to the view using ViewBag
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

