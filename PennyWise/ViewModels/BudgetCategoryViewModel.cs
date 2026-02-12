using System.ComponentModel.DataAnnotations;

namespace PennyWise.ViewModels
{
    public class BudgetCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}

