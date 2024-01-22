using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models.Reports
{
    public class TransactionsByBankNameModel
    {
        [DisplayName("№")]
        public int Id { get; set; }

        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("Amount")]
        public double Amount { get; set; }

        [DisplayName("Bank name")]
        public string? BankName { get; set; }

        [DisplayName("Bank phone")]
        public string? BankPhone { get; set; }

        [DisplayName("Bank address")]
        public string? BankAddress { get; set; }

        [DisplayName("Account number")]
        public Guid? AccountNumber { get; set; }

        [DisplayName("Account name")]
        public string? AccountName { get; set; }
    }
}
