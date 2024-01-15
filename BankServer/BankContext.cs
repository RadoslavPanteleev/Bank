using BankServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankServer
{
    public class BankContext : IdentityDbContext<Person>
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }

        // Entities
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Person> Peoples { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionsTypes { get; set; }
    }
}
