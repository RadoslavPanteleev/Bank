using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? AccountNumber { get; set; }

        [Required]
        public Person? Person { get; set; } 
    }
}
