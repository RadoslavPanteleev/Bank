using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class LocationInputModel
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public int AddressId { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
