using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Controllers.Models
{
    public class AccountInputModel
    {
        [Required]
        public Guid? AccountNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string? AccountName { get; set; }
    }
}
