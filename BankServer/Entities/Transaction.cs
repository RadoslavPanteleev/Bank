using BankServer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Entities
{
    public class Transaction : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public int TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; }

        [Required]
        public int BankId { get; set; }
        public Bank? Bank { get; set; }

        [Required]
        public int LocationId { get; set; }
        public Location? Location { get; set; }

        [Required]
        public Guid? AccountId { get; set; }
        public Account? Account { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
