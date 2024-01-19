using BankServer.Entities;

namespace BankServer.Models
{
    public class TransactionOutputModel
    {
        public int TransactionId { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public TransactionType? TransactionType { get; set; }

        public Bank? Bank { get; set; }

        public Location? Location { get; set; }

        public Guid? AccountNumber { get; set; }

        public Category? Category { get; set; }
    }
}
