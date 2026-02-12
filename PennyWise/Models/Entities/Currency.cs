using System.ComponentModel.DataAnnotations;

namespace PennyWise.Models.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(3)]
        public string Code { get; set; } = null!; // USD, EUR, INR

        [Required]
        [MaxLength(5)]
        public string Symbol { get; set; } = null!; // $, €, ₹

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!; // US Dollar, Euro, Indian Rupee

        public bool IsActive { get; set; } = true;
    }
}
