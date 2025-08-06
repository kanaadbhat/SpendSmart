
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Models
{
    public class Expense
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; } = null!;
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        
        // Foreign key for Category
        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
        
        // Navigation property (do not validate)
        public virtual Category? Category { get; set; }
    }
}
