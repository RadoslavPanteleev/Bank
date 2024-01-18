using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Transaction
    {
        public int ID { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public TransactionType? Type { get; set; }

        [Required]
        public Bank? Bank { get; set; }

        [Required]
        public Location? Location { get; set; }

        [Required]
        public Account? Account { get; set; }

        [Required]
        public Category? Category { get; set; }
    }
}
