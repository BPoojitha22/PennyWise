using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PennyWise.Interfaces;
using PennyWise.Models.Entities;

namespace PennyWise.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public Task<IEnumerable<BudgetCategory>> GetCategoriesAsync(int userId)
        {
            return _budgetRepository.GetCategoriesAsync(userId);
        }

        public Task<BudgetCategory?> GetCategoryByIdAsync(int id, int userId)
        {
            return _budgetRepository.GetCategoryByIdAsync(id, userId);
        }

        public async Task CreateCategoryAsync(int userId, string name)
        {
            var category = new BudgetCategory
            {
                UserId = userId,
                Name = name,
                IsActive = true
            };

            await _budgetRepository.AddCategoryAsync(category);
            await _budgetRepository.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(BudgetCategory category, string name)
        {
            category.Name = name;
            await _budgetRepository.UpdateCategoryAsync(category);
            await _budgetRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(BudgetCategory category)
        {
            await _budgetRepository.DeleteCategoryAsync(category);
            await _budgetRepository.SaveChangesAsync();
        }

        public Task<IEnumerable<BudgetAllocation>> GetAllocationsForMonthAsync(int userId, int month, int year)
        {
            return _budgetRepository.GetAllocationsForMonthAsync(userId, month, year);
        }

        public async Task SaveAllocationsForMonthAsync(int userId, int month, int year, IDictionary<int, decimal> categoryAllocations)
        {
            var existing = (await _budgetRepository.GetAllocationsForMonthAsync(userId, month, year)).ToList();

            foreach (var kvp in categoryAllocations)
            {
                var categoryId = kvp.Key;
                var amount = kvp.Value;

                var allocation = existing.SingleOrDefault(a => a.BudgetCategoryId == categoryId);
                if (allocation == null)
                {
                    allocation = new BudgetAllocation
                    {
                        UserId = userId,
                        BudgetCategoryId = categoryId,
                        Month = month,
                        Year = year,
                        AllocatedAmount = amount
                    };
                    await _budgetRepository.AddAllocationAsync(allocation);
                }
                else
                {
                    allocation.AllocatedAmount = amount;
                    await _budgetRepository.UpdateAllocationAsync(allocation);
                }
            }

            await _budgetRepository.SaveChangesAsync();
        }
    }
}

