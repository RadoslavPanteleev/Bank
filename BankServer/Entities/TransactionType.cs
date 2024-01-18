using BankServer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Entities
{
    /// <summary>
    /// TransactionType
    /// </summary>
    public class TransactionType : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? Type { get; set; }
    }
}
