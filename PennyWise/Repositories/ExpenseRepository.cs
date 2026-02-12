using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PennyWise.Data;
using PennyWise.Interfaces;
using PennyWise.Models.Entities;

namespace PennyWise.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetForMonthAsync(int userId, int month, int year)
        {
            return await _context.Expenses
                .Include(e => e.BudgetCategory)
                .Where(e => e.UserId == userId &&
                            e.Date.Month == month &&
                            e.Date.Year == year)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetForDateRangeAsync(int userId, DateTime from, DateTime to)
        {
            return await _context.Expenses
                .Include(e => e.BudgetCategory)
                .Where(e => e.UserId == userId &&
                            e.Date >= from &&
                            e.Date <= to)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

