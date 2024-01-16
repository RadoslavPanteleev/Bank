using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    [Index(nameof(Account), IsUnique = true)]
    public class PersonAccount
    {
        [Required]
        public Person? Person { get; set; }

        [Required]
        public Account? Account { get; set; }
    }
}
