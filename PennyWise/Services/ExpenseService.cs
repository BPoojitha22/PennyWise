using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PennyWise.Interfaces;
using PennyWise.Models.Entities;

namespace PennyWise.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public Task<IEnumerable<Expense>> GetForMonthAsync(int userId, int month, int year)
        {
            return _expenseRepository.GetForMonthAsync(userId, month, year);
        }

        public async Task AddAsync(int userId, int categoryId, DateTime date, decimal amount, string? description)
        {
            var expense = new Expense
            {
                UserId = userId,
                BudgetCategoryId = categoryId,
                Date = date,
                Amount = amount,
                Description = description
            };

            await _expenseRepository.AddAsync(expense);
            await _expenseRepository.SaveChangesAsync();
        }
    }
}

