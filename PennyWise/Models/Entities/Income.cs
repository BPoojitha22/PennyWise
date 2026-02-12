using System;

namespace PennyWise.Models.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Month { get; set; } // 1-12
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
    }
}

