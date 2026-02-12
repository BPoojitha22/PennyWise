using System.Linq;
using System.Threading.Tasks;
using PennyWise.Interfaces;
using PennyWise.ViewModels;

namespace PennyWise.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IExpenseRepository _expenseRepository;

        public DashboardService(
            IIncomeRepository incomeRepository,
            IBudgetRepository budgetRepository,
            IExpenseRepository expenseRepository)
        {
            _incomeRepository = incomeRepository;
            _budgetRepository = budgetRepository;
            _expenseRepository = expenseRepository;
        }

        public async Task<DashboardViewModel> GetMonthlySummaryAsync(int userId, int month, int year)
        {
            var income = await _incomeRepository.GetForMonthAsync(userId, month, year);
            
            // Get ALL user categories (not just those with allocations)
            var allCategories = await _budgetRepository.GetCategoriesAsync(userId);
            var allocations = (await _budgetRepository.GetAllocationsForMonthAsync(userId, month, year)).ToList();
            var expenses = (await _expenseRepository.GetForMonthAsync(userId, month, year)).ToList();

            // Build summary for ALL categories
            var categorySummaries = allCategories
                .Select(cat => new CategorySummaryViewModel
                {
                    CategoryId = cat.Id,
                    CategoryName = cat.Name,
                    Allocated = allocations.FirstOrDefault(a => a.BudgetCategoryId == cat.Id)?.AllocatedAmount ?? 0,
                    Spent = expenses.Where(e => e.BudgetCategoryId == cat.Id).Sum(e => e.Amount)
                })
                .ToList();

            var totalAllocated = categorySummaries.Sum(c => c.Allocated);
            var totalSpent = categorySummaries.Sum(c => c.Spent);
            var incomeAmount = income?.Amount ?? 0;

            var vm = new DashboardViewModel
            {
                Month = month,
                Year = year,
                Income = incomeAmount,
                TotalAllocated = totalAllocated,
                TotalSpent = totalSpent,
                TotalSavings = incomeAmount - totalAllocated,
                BalanceLeft = incomeAmount - totalSpent,
                CategorySummaries = categorySummaries
            };

            return vm;
        }
    }
}

