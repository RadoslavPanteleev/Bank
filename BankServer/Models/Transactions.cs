namespace BankServer.Models
{
    public class Transactions
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public float Amount { get; set; }

        public int TransactionTypeId { get; set; }

        public int BankId { get; set; }

        public int LocationId { get; set; }

        public int PersonId { get; set; }

        public int CategoryId { get; set; }
    }
}
