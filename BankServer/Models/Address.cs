using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Description { get; set; }
    }
}
