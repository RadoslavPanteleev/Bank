using BankServer.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Controllers.Models
{
    public class AccountInputModel
    {
        [SwaggerSchema(Description = "Field for internal use only. Pass it with 0 only when creating new record.", Nullable = true, Format = null)]
        public int UpdateCounter { get; set; } = 0;

        [Required]
        [StringLength(50)]
        public string? AccountNumber { get; set; }

        [Required]
        public string? PersonID { get; set; }
    }
}
