using BankServer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Account : BaseModel
    {
        [Required]
        [Key]
        public string? AccountNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string? AccountName { get; set; }

        [Required]
        public Person? Person { get; set; } 
    }
}
