using Microsoft.EntityFrameworkCore;
using PennyWise.Models.Entities;

namespace PennyWise.Data
{
    // EF Core DbContext mapped to manually created SQL Server schema
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Income> Incomes { get; set; } = null!;
        public DbSet<BudgetCategory> BudgetCategories { get; set; } = null!;
        public DbSet<BudgetAllocation> BudgetAllocations { get; set; } = null!;
        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<Currency> Currencies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currencies");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Code).HasMaxLength(3).IsRequired();
                entity.Property(c => c.Symbol).HasMaxLength(5).IsRequired();
                entity.Property(c => c.Name).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(e => e.UserName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(200).IsRequired();
                entity.Property(e => e.PasswordHash).HasMaxLength(500).IsRequired();
                entity.HasOne(e => e.Currency)
                      .WithMany()
                      .HasForeignKey(e => e.CurrencyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.ToTable("Incomes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BudgetCategory>(entity =>
            {
                entity.ToTable("BudgetCategories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BudgetAllocation>(entity =>
            {
                entity.ToTable("BudgetAllocations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AllocatedAmount).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.BudgetCategory)
                    .WithMany(c => c.BudgetAllocations)
                    .HasForeignKey(e => e.BudgetCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("Expenses");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.BudgetCategory)
                    .WithMany(c => c.Expenses)
                    .HasForeignKey(e => e.BudgetCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

