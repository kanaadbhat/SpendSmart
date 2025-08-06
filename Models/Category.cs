using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters")]
        public string Name { get; set; } = null!;
        
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
        public string? Description { get; set; }
        
        public string Color { get; set; } = "#3B82F6"; // Default blue color
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation property
        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
} 