using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data.Service
{
    public class ExpensesService : IExpensesService
    {
        private readonly FinanceAppContext _context;

        public ExpensesService(FinanceAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _context.Expenses
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetAllWithCategoriesAsync()
        {
            return await _context.Expenses
                .Include(e => e.Category)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Expense> CreateAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense> UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task DeleteAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> GetTotalExpensesForCurrentMonthAsync()
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var result = await _context.Expenses
                .Where(e => e.Date >= startOfMonth && e.Date <= endOfMonth)
                .SumAsync(e => e.Amount);
            
            return result;
        }

        public async Task<IEnumerable<Expense>> GetRecentExpensesAsync(int count = 10)
        {
            return await _context.Expenses
                .Include(e => e.Category)
                .OrderByDescending(e => e.Date)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetChartDataAsync()
        {
            var data = await _context.Expenses
                .Include(e => e.Category)
                .GroupBy(e => new { e.Category.Id, e.Category.Name, e.Category.Color })
                .Select(g => new
                {
                    CategoryId = g.Key.Id,
                    CategoryName = g.Key.Name,
                    CategoryColor = g.Key.Color,
                    Total = g.Sum(e => e.Amount)
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync();

            return data.Cast<object>();
        }

        public async Task<IEnumerable<Expense>> SearchExpensesAsync(string searchTerm, DateTime? startDate = null, DateTime? endDate = null, int? categoryId = null)
        {
            var query = _context.Expenses.Include(e => e.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(e => e.Description.Contains(searchTerm));
            }

            if (startDate.HasValue)
            {
                query = query.Where(e => e.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(e => e.Date <= endDate.Value);
            }

            if (categoryId.HasValue)
            {
                query = query.Where(e => e.CategoryId == categoryId.Value);
            }

            return await query.OrderByDescending(e => e.Date).ToListAsync();
        }

        // Legacy methods for backward compatibility
        public async Task<IEnumerable<Expense>> GetAll()
        {
            return await GetAllAsync();
        }

        public async Task Add(Expense expense)
        {
            await CreateAsync(expense);
        }
    }
}
