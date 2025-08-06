using FinanceApp.Data.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FinanceApp.Models;

namespace FinanceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExpensesService _expensesService;

        public HomeController(IExpensesService expensesService)
        {
            _expensesService = expensesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalExpensesThisMonth = await _expensesService.GetTotalExpensesForCurrentMonthAsync();
            ViewBag.RecentExpenses = await _expensesService.GetRecentExpensesAsync(5);
            ViewBag.ChartData = await _expensesService.GetChartDataAsync();
            
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
