using BankServer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Entities
{
    public class Address : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string? Description { get; set; }
    }
}
