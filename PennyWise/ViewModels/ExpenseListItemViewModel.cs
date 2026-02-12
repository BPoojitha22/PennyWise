using System;

namespace PennyWise.ViewModels
{
    public class ExpenseListItemViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CategoryName { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}

