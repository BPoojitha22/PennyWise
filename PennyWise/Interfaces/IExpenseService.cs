using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PennyWise.Models.Entities;

namespace PennyWise.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetForMonthAsync(int userId, int month, int year);
        Task AddAsync(int userId, int categoryId, DateTime date, decimal amount, string? description);
    }
}

