using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class AccountInputModel
    {
        [Required]
        [StringLength(50)]
        public string? AccountName { get; set; }
    }
}
