using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PennyWise.ViewModels
{
    public class BudgetAllocationItem
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public decimal AllocatedAmount { get; set; }
    }

    public class BudgetAllocationEditViewModel
    {
        [Range(1, 12)]
        public int Month { get; set; }

        public int Year { get; set; }

        public IList<BudgetAllocationItem> Items { get; set; } = new List<BudgetAllocationItem>();
    }
}

