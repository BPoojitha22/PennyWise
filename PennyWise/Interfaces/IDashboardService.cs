using System.Threading.Tasks;
using PennyWise.ViewModels;

namespace PennyWise.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetMonthlySummaryAsync(int userId, int month, int year);
    }
}

