using System.Collections.Generic;
using System.Threading.Tasks;
using PennyWise.Models.Entities;

namespace PennyWise.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<BudgetCategory>> GetCategoriesAsync(int userId);
        Task<BudgetCategory?> GetCategoryByIdAsync(int id, int userId);
        Task CreateCategoryAsync(int userId, string name);
        Task UpdateCategoryAsync(BudgetCategory category, string name);
        Task DeleteCategoryAsync(BudgetCategory category);

        Task<IEnumerable<BudgetAllocation>> GetAllocationsForMonthAsync(int userId, int month, int year);
        Task SaveAllocationsForMonthAsync(int userId, int month, int year, IDictionary<int, decimal> categoryAllocations);
    }
}

