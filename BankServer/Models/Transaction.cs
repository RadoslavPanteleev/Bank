using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Transaction
    {
        public int ID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public Bank Bank { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        public Person Person { get; set; }

        [Required]
        public Category Category { get; set; }

        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
