using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Address
    {
        public int Id { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(200)]
        public string? Description { get; set; }
    }
}
