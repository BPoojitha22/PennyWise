using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PennyWise.Data;
using PennyWise.Interfaces;
using PennyWise.Models.Entities;

namespace PennyWise.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDbContext _context;

        public IncomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Income?> GetForMonthAsync(int userId, int month, int year)
        {
            return _context.Incomes
                .SingleOrDefaultAsync(i => i.UserId == userId && i.Month == month && i.Year == year);
        }

        public async Task AddAsync(Income income)
        {
            await _context.Incomes.AddAsync(income);
        }

        public Task UpdateAsync(Income income)
        {
            _context.Incomes.Update(income);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Income>> GetForUserAsync(int userId)
        {
            return await _context.Incomes
                .Where(i => i.UserId == userId)
                .OrderByDescending(i => i.Year).ThenByDescending(i => i.Month)
                .ToListAsync();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

