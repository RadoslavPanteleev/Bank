using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Person : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string AccountNumber { get; set; }
    }
}
