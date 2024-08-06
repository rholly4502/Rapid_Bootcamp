using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidBootcamp.WebApplication.Models
{
    public class Customer
    {
        //autoincrement false

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(255)]
        public string CustomerName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Address { get; set; } = null!;

        public IEnumerable<OrderHeader>? OrderHeaders { get; set; }
    }
}
