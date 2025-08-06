using FinanceApp.Data.Service;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;
        private readonly ICategoryService _categoryService;

        public ExpensesController(IExpensesService expensesService, ICategoryService categoryService)
        {
            _expensesService = expensesService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string searchTerm, DateTime? startDate, DateTime? endDate, int? categoryId)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.SearchTerm = searchTerm;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.CategoryId = categoryId;

            var expenses = await _expensesService.SearchExpensesAsync(searchTerm, startDate, endDate, categoryId);
            return View(expenses);
        }

        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalExpensesThisMonth = await _expensesService.GetTotalExpensesForCurrentMonthAsync();
            ViewBag.RecentExpenses = await _expensesService.GetRecentExpensesAsync(10);
            ViewBag.ChartData = await _expensesService.GetChartDataAsync();
            
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            // Debug: Log the received expense data
            Console.WriteLine($"Received expense: Description={expense.Description}, Amount={expense.Amount}, CategoryId={expense.CategoryId}, Date={expense.Date}");
            
            if (ModelState.IsValid)
            {
                await _expensesService.CreateAsync(expense);
                TempData["Success"] = "Expense added successfully!";
                return RedirectToAction(nameof(Index));
            }

            // Debug: Log validation errors
            foreach (var error in ModelState)
            {
                Console.WriteLine($"Validation error for {error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(expense);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expensesService.GetByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            // Debug: Log the received expense data
            Console.WriteLine($"Editing expense {id}: Description={expense.Description}, Amount={expense.Amount}, CategoryId={expense.CategoryId}, Date={expense.Date}");

            if (ModelState.IsValid)
            {
                try
                {
                    await _expensesService.UpdateAsync(expense);
                    TempData["Success"] = "Expense updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating expense: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while updating the expense.");
                }
            }

            // Debug: Log validation errors
            foreach (var error in ModelState)
            {
                Console.WriteLine($"Validation error for {error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(expense);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _expensesService.GetByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _expensesService.DeleteAsync(id);
                TempData["Success"] = "Expense deleted successfully!";
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while deleting the expense.";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetChart()
        {
            var data = await _expensesService.GetChartDataAsync();
            return Json(data);
        }
    }
}
