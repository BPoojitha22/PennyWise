namespace PennyWise.Models.Entities
{
    public class BudgetAllocation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BudgetCategoryId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal AllocatedAmount { get; set; }

        public User User { get; set; } = null!;
        public BudgetCategory BudgetCategory { get; set; } = null!;
    }
}

