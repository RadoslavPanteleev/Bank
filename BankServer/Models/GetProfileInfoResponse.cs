using BankServer.Entities;

namespace BankServer.Models
{
    public class GetProfileInfoResponse
    {
        public string? Id { get; set; }

        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public Address? Address { get; set; }

        public int AccessFailedCount { get; set; }
    }
}
