﻿using System.ComponentModel.DataAnnotations;

namespace BankServer.Models
{
    public class Location
    {
        public int Id { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

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
