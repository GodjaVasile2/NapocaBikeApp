using System.ComponentModel.DataAnnotations;

namespace NapocaBike.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Category name cannot be longer than 100 characters.")]
        public string CategoryName { get; set; }
        public ICollection<LocationCategory>? LocationCategories { get; set; }
    }
}
