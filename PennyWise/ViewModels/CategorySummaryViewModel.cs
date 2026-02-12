namespace PennyWise.ViewModels
{
    public class CategorySummaryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public decimal Allocated { get; set; }
        public decimal Spent { get; set; }
        public decimal Remaining => Allocated - Spent;
        public bool IsOverspent => Spent > Allocated && Allocated > 0;
    }
}

