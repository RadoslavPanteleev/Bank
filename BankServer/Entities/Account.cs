using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankServer.Entities
{
    public class Account
    {
        [Required]
        [Key]
        public Guid? AccountNumber { get; set; }

        [Timestamp]
        [JsonIgnore]
        public byte[]? Version { get; set; }

        [Required]
        [MaxLength(100)]
        public string? AccountName { get; set; }

        [Required]
        public string? PersonId { get; set; } 
        public Person? Person { get; set; }
    }
}
