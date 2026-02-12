using System.Collections.Generic;
using System.Threading.Tasks;
using PennyWise.Models.Entities;

namespace PennyWise.Interfaces
{
    public interface IIncomeRepository
    {
        Task<Income?> GetForMonthAsync(int userId, int month, int year);
        Task AddAsync(Income income);
        Task UpdateAsync(Income income);
        Task<IEnumerable<Income>> GetForUserAsync(int userId);
        Task SaveChangesAsync();
    }
}

