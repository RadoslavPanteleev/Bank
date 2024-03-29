﻿namespace BankServer.Models
{
    public class EditProfileInput
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public int AddressId { get; set; } = 0;
    }
}
