using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Phone { get; set; }
    }
}
