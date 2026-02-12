using System.Collections.Generic;

namespace PennyWise.ViewModels
{
    public class DashboardViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }

        public decimal Income { get; set; }
        public decimal TotalAllocated { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal TotalSavings { get; set; }
        public decimal BalanceLeft { get; set; }

        public IList<CategorySummaryViewModel> CategorySummaries { get; set; } = new List<CategorySummaryViewModel>();
    }
}

