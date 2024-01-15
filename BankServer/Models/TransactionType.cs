using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class TransactionType
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }
    }
}
