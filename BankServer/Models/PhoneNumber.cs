using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100)]
        public string? Phone { get; set; }
    }
}
