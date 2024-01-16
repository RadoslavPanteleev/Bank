using BankServer.Models.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    /// <summary>
    /// TransactionType
    /// </summary>
    public class TransactionType : BaseModel
    {
        [SwaggerSchema(Description = "Field for internal use only", Nullable = true)]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string? Type { get; set; }
    }
}
