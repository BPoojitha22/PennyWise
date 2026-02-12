using System.Threading.Tasks;
using PennyWise.Models.Entities;

namespace PennyWise.Interfaces
{
    public interface IIncomeService
    {
        Task<Income?> GetForMonthAsync(int userId, int month, int year);
        Task SaveForMonthAsync(int userId, int month, int year, decimal amount);
    }
}

