using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models.Reports
{
    public class TransactionsByLocationNameModel
    {
        [DisplayName("№")]
        public int Id { get; set; }

        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("Amount")]
        public double Amount { get; set; }

        [DisplayName("Type")]
        public string? Type { get; set; }

        [DisplayName("Bank name")]
        public string? BankName { get; set; }

        [DisplayName("Location name")]
        public string? LocationName { get; set; }

        [DisplayName("Location address")]
        public string? LocationAddress { get; set; }

        [DisplayName("Account number")]
        public Guid? AccountNumber { get; set; }

        [DisplayName("Account name")]
        public string? AccountName { get; set; }

        [DisplayName("Category name")]
        public string? CategoryName { get; set; }
    }
}
