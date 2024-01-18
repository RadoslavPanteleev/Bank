using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Account
    {
        [Required]
        [Key]
        public Guid? AccountNumber { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string? AccountName { get; set; }

        [Required]
        public string? PersonId { get; set; } 
    }
}
