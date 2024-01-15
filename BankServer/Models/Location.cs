using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Location
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
