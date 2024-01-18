using BankServer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankServer
{
    public class AppDbContext : IdentityDbContext<Person>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Entities
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Bank> Banks { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Person> Peoples { get; set; } = null!;
        public DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<TransactionType> TransactionsTypes { get; set; } = null!;
    }
}
