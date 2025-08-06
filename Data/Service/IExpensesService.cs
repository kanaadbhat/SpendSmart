using FinanceApp.Models;

namespace FinanceApp.Data.Service
{
    public interface IExpensesService
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<IEnumerable<Expense>> GetAllWithCategoriesAsync();
        Task<Expense?> GetByIdAsync(int id);
        Task<Expense> CreateAsync(Expense expense);
        Task<Expense> UpdateAsync(Expense expense);
        Task DeleteAsync(int id);
        Task<decimal> GetTotalExpensesForCurrentMonthAsync();
        Task<IEnumerable<Expense>> GetRecentExpensesAsync(int count = 10);
        Task<IEnumerable<object>> GetChartDataAsync();
        Task<IEnumerable<Expense>> SearchExpensesAsync(string searchTerm, DateTime? startDate = null, DateTime? endDate = null, int? categoryId = null);
    }
}
