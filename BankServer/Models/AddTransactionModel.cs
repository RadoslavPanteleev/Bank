using BankServer.Entities;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class AddTransactionModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int BankId { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public Guid AccountNumber { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
