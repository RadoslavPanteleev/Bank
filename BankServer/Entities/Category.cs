using BankServer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string? Description { get; set; }
    }
}
