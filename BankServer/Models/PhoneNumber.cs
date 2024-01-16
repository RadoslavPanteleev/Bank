using BankServer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class PhoneNumber : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Phone { get; set; }
    }
}
