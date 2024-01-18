using System.ComponentModel.DataAnnotations;

namespace BankServer.Entities
{
    public class Account
    {
        [Required]
        [Key]
        public Guid? AccountNumber { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string? AccountName { get; set; }

        [Required]
        public string? PersonId { get; set; } 
        public Person? Person { get; set; }
    }
}
