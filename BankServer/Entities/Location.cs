using BankServer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Entities
{
    public class Location : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public int AddressId { get; set; }
        public Address? Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
