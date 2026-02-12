using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PennyWise.Data;
using PennyWise.Interfaces;
using PennyWise.Models.Entities;

namespace PennyWise.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BudgetCategory>> GetCategoriesAsync(int userId)
        {
            return await _context.BudgetCategories
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public Task<BudgetCategory?> GetCategoryByIdAsync(int id, int userId)
        {
            return _context.BudgetCategories
                .SingleOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }

        public async Task AddCategoryAsync(BudgetCategory category)
        {
            await _context.BudgetCategories.AddAsync(category);
        }

        public Task UpdateCategoryAsync(BudgetCategory category)
        {
            _context.BudgetCategories.Update(category);
            return Task.CompletedTask;
        }

        public Task DeleteCategoryAsync(BudgetCategory category)
        {
            _context.BudgetCategories.Remove(category);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<BudgetAllocation>> GetAllocationsForMonthAsync(int userId, int month, int year)
        {
            return await _context.BudgetAllocations
                .Include(a => a.BudgetCategory)
                .Where(a => a.UserId == userId && a.Month == month && a.Year == year)
                .ToListAsync();
        }

        public Task<BudgetAllocation?> GetAllocationAsync(int userId, int categoryId, int month, int year)
        {
            return _context.BudgetAllocations
                .SingleOrDefaultAsync(a =>
                    a.UserId == userId &&
                    a.BudgetCategoryId == categoryId &&
                    a.Month == month &&
                    a.Year == year);
        }

        public async Task AddAllocationAsync(BudgetAllocation allocation)
        {
            await _context.BudgetAllocations.AddAsync(allocation);
        }

        public Task UpdateAllocationAsync(BudgetAllocation allocation)
        {
            _context.BudgetAllocations.Update(allocation);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

