using BankServer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Entities
{
    public class Bank : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(200)]
        public string? Address { get; set; }

        [Required]
        public int PhoneNumberId { get; set; }
        public PhoneNumber? PhoneNumber{ get; set; }
    }
}
