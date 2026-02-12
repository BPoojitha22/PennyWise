using System.ComponentModel.DataAnnotations;

namespace PennyWise.ViewModels
{
    public class IncomeEditViewModel
    {
        [Range(1, 12)]
        public int Month { get; set; }

        public int Year { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}

