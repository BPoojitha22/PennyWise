using System;

namespace PennyWise.Models.Entities
{
    // Simple user entity for form-based authentication
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public int? CurrencyId { get; set; }
        public Currency? Currency { get; set; }
    }
}

