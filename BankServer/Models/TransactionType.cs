using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    /// <summary>
    /// TransactionType
    /// </summary>
    public class TransactionType
    {
        [SwaggerSchema(Description = "Field for internal use only", Nullable = true)]
        public int ID { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100)]
        public string? Type { get; set; }
    }
}
