using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PennyWise.Models.Entities;

namespace PennyWise.Interfaces
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetForMonthAsync(int userId, int month, int year);
        Task<IEnumerable<Expense>> GetForDateRangeAsync(int userId, DateTime from, DateTime to);
        Task AddAsync(Expense expense);
        Task SaveChangesAsync();
    }
}

