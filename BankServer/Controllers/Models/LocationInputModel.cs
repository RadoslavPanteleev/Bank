using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Controllers.Models
{
    public class LocationInputModel
    {
        [SwaggerSchema(Description = "Field for internal use only. Pass it with 0 only when creating new record.", Nullable = true, Format = null)]
        public int UpdateCounter { get; set; } = 0;

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
