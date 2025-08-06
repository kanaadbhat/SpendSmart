using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data
{
    public class FinanceAppContext : DbContext
    {
        public FinanceAppContext(DbContextOptions<FinanceAppContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(100);
                entity.Property(e => e.Color).HasMaxLength(7); // Hex color code
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Configure Expense entity
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Date).HasDefaultValueSql("GETDATE()");

                // Configure relationship with Category
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Expenses)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed default categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food & Dining", Description = "Restaurants, groceries, and dining out", Color = "#EF4444", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Category { Id = 2, Name = "Transportation", Description = "Gas, public transport, and vehicle expenses", Color = "#3B82F6", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Category { Id = 3, Name = "Shopping", Description = "Clothing, electronics, and general shopping", Color = "#10B981", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Category { Id = 4, Name = "Entertainment", Description = "Movies, games, and leisure activities", Color = "#F59E0B", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Category { Id = 5, Name = "Bills & Utilities", Description = "Electricity, water, internet, and phone bills", Color = "#8B5CF6", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Category { Id = 6, Name = "Healthcare", Description = "Medical expenses and health-related costs", Color = "#EC4899", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Category { Id = 7, Name = "Education", Description = "Books, courses, and educational expenses", Color = "#06B6D4", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Category { Id = 8, Name = "Other", Description = "Miscellaneous expenses", Color = "#6B7280", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Seed dummy expenses for the last 12 months
            modelBuilder.Entity<Expense>().HasData(
                new Expense { Id = 1, Description = "Groceries", Amount = 120.50m, Date = new DateTime(2024, 9, 10), CategoryId = 1 },
                new Expense { Id = 2, Description = "Bus Pass", Amount = 45.00m, Date = new DateTime(2024, 10, 5), CategoryId = 2 },
                new Expense { Id = 3, Description = "New Shoes", Amount = 80.00m, Date = new DateTime(2024, 11, 15), CategoryId = 3 },
                new Expense { Id = 4, Description = "Movie Night", Amount = 30.00m, Date = new DateTime(2024, 12, 20), CategoryId = 4 },
                new Expense { Id = 5, Description = "Electricity Bill", Amount = 60.00m, Date = new DateTime(2025, 1, 2), CategoryId = 5 },
                new Expense { Id = 6, Description = "Doctor Visit", Amount = 100.00m, Date = new DateTime(2025, 2, 12), CategoryId = 6 },
                new Expense { Id = 7, Description = "Online Course", Amount = 150.00m, Date = new DateTime(2025, 3, 8), CategoryId = 7 },
                new Expense { Id = 8, Description = "Miscellaneous", Amount = 40.00m, Date = new DateTime(2025, 4, 18), CategoryId = 8 },
                new Expense { Id = 9, Description = "Dinner Out", Amount = 75.00m, Date = new DateTime(2025, 5, 22), CategoryId = 1 },
                new Expense { Id = 10, Description = "Taxi Ride", Amount = 25.00m, Date = new DateTime(2025, 6, 3), CategoryId = 2 },
                new Expense { Id = 11, Description = "Clothes Shopping", Amount = 200.00m, Date = new DateTime(2025, 7, 14), CategoryId = 3 },
                new Expense { Id = 12, Description = "Concert Ticket", Amount = 90.00m, Date = new DateTime(2025, 8, 1), CategoryId = 4 },
                new Expense { Id = 13, Description = "Water Bill", Amount = 35.00m, Date = new DateTime(2025, 8, 2), CategoryId = 5 },
                new Expense { Id = 14, Description = "Pharmacy", Amount = 60.00m, Date = new DateTime(2025, 8, 3), CategoryId = 6 },
                new Expense { Id = 15, Description = "Book Purchase", Amount = 25.00m, Date = new DateTime(2025, 8, 4), CategoryId = 7 },
                new Expense { Id = 16, Description = "Gift", Amount = 50.00m, Date = new DateTime(2025, 8, 5), CategoryId = 8 }
            );
        }
    }
}
