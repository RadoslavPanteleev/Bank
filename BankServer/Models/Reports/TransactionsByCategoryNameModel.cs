using System.ComponentModel;

namespace BankServer.Models.Reports
{
    public class TransactionsByCategoryNameModel
    {
        [DisplayName("№")]
        public int Id { get; set; }

        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("Amount")]
        public double Amount { get; set; }

        [DisplayName("Type")]
        public string? Type { get; set; }

        [DisplayName("Account number")]
        public Guid? AccountNumber { get; set; }

        [DisplayName("Account name")]
        public string? AccountName { get; set; }

        [DisplayName("Category name")]
        public string? CategoryName { get; set; }
    }
}
