﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Entities
{
    public class Person : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }

        [Required]
        public new PhoneNumber? PhoneNumber { get; set; }

        public int AddressId { get; set; }
    }
}
