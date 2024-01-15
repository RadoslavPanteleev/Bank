using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        //public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
