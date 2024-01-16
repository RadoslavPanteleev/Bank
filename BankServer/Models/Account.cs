using BankServer.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankServer.Models
{
    public class Account : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? AccountNumber { get; set; }

        [Required]
        public Person? Person { get; set; } 
    }
}
