using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Controllers.Models
{
    public class AccountInputModel
    {
        [Required]
        public string? AccountNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string? AccountName { get; set; }

        [SwaggerSchema(Description = "Field for internal use only. Pass it with 0 only when creating new record.", Nullable = true, Format = null)]
        public int UpdateCounter { get; set; } = 0;
    }
}
