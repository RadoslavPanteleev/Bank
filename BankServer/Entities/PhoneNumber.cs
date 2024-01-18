using BankServer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Entities
{
    public class PhoneNumber : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? Phone { get; set; }
    }
}
