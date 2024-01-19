using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class BankInputModel
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(200)]
        public string? Address { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
    }
}
