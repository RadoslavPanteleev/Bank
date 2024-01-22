using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models.Reports
{
    public class TransactionsByPersonNameModel
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

        [DisplayName("First name")]
        public string? FirstName { get; set; }

        [DisplayName("Last name")]
        public string? LastName { get; set; }

        [DisplayName("Phone number")]
        public string? Phone { get; set; }
    }
}
