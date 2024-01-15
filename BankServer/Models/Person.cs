using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string AccountNumber { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
