using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Bank
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
