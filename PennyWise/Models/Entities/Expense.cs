using System;

namespace PennyWise.Models.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BudgetCategoryId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }

        public User User { get; set; } = null!;
        public BudgetCategory BudgetCategory { get; set; } = null!;
    }
}

