using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Bank
    {
        public int ID { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(200)]
        public string? Address { get; set; }

        [Required]
        public PhoneNumber? PhoneNumber{ get; set; }
    }
}
