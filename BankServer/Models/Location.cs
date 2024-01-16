using BankServer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Location : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public Address? Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
