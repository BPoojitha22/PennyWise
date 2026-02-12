using System.Collections.Generic;

namespace PennyWise.Models.Entities
{
    public class BudgetCategory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public User User { get; set; } = null!;
        public ICollection<BudgetAllocation> BudgetAllocations { get; set; } = new List<BudgetAllocation>();
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}

