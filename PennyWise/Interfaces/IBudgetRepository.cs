using System.Collections.Generic;
using System.Threading.Tasks;
using PennyWise.Models.Entities;

namespace PennyWise.Interfaces
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<BudgetCategory>> GetCategoriesAsync(int userId);
        Task<BudgetCategory?> GetCategoryByIdAsync(int id, int userId);
        Task AddCategoryAsync(BudgetCategory category);
        Task UpdateCategoryAsync(BudgetCategory category);
        Task DeleteCategoryAsync(BudgetCategory category);

        Task<IEnumerable<BudgetAllocation>> GetAllocationsForMonthAsync(int userId, int month, int year);
        Task<BudgetAllocation?> GetAllocationAsync(int userId, int categoryId, int month, int year);
        Task AddAllocationAsync(BudgetAllocation allocation);
        Task UpdateAllocationAsync(BudgetAllocation allocation);

        Task SaveChangesAsync();
    }
}

