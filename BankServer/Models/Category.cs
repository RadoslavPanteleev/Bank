using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Category
    {
        public int ID { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string? Description { get; set; }
    }
}
