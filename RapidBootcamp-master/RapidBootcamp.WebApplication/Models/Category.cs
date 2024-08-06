using System.ComponentModel.DataAnnotations;

namespace RapidBootcamp.WebApplication.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; } = null!;

        //navigation property
        public IEnumerable<Product>? Products { get; set; }
    }
}
