using System.Threading.Tasks;
using PennyWise.Interfaces;
using PennyWise.Models.Entities;

namespace PennyWise.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public Task<Income?> GetForMonthAsync(int userId, int month, int year)
        {
            return _incomeRepository.GetForMonthAsync(userId, month, year);
        }

        public async Task SaveForMonthAsync(int userId, int month, int year, decimal amount)
        {
            var existing = await _incomeRepository.GetForMonthAsync(userId, month, year);
            if (existing == null)
            {
                existing = new Income
                {
                    UserId = userId,
                    Month = month,
                    Year = year,
                    Amount = amount
                };
                await _incomeRepository.AddAsync(existing);
            }
            else
            {
                existing.Amount = amount;
                await _incomeRepository.UpdateAsync(existing);
            }

            await _incomeRepository.SaveChangesAsync();
        }
    }
}

