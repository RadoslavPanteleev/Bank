using BankServer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Category : BaseModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string? Description { get; set; }
    }
}
