using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models.Reports
{
    public class TransactionsHistoryModel
    {
        [DisplayName("№")]
        public int Id { get; set; }

        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("Amount")]
        public double Amount { get; set; }
    }
}
