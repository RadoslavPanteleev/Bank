using BankServer;
using BankServer.Entities.Configurations;
using BankServer.Entities;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TestDataGenerator
{
    public partial class AppDbContextTest : AppDbContext
    {
        private void GenerateFakeTestData(ModelBuilder builder)
        {
            // 100 users generate
            var faker = new Faker<BankServer.Entities.Person>()
                .RuleFor(u => u.Id, f => f.Random.Guid().ToString())
                .RuleFor(u => u.UserName, f => f.Internet.UserName().ToLower())
                .RuleFor(u => u.NormalizedUserName, (f, u) => u.UserName.ToUpper())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Internet.Email().ToLower())
                .RuleFor(u => u.NormalizedEmail, (f, u) => u.Email.ToUpper())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.EmailConfirmed, true)
                .RuleFor(u => u.PhoneNumberConfirmed, true)
                .RuleFor(u => u.SecurityStamp, f => f.Random.Guid().ToString("D"))
                .RuleFor(u => u.PasswordHash, (f, u) => new PasswordHasher<BankServer.Entities.Person>().HashPassword(u, "Test1234."));
            ;

            var users = faker.Generate(100);
            builder.Entity<BankServer.Entities.Person>().HasData(users);

            var roles = users.SelectMany(user =>
                Enumerable.Range(1, 1).Select(i => new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = RoleConfiguration.UserRoleId
                })
            ).ToList();
            builder.Entity<IdentityUserRole<string>>().HasData(roles);

            // Random 1 - 10 accounts for 100 users
            var accounts = users.SelectMany(user =>
            Enumerable.Range(1, new Faker().Random.Number(1, 10)).Select(i => new Account
            {
                AccountNumber = Guid.NewGuid(),
                AccountName = new Faker().Finance.AccountName(),
                PersonId = user.Id
            })
            ).ToList();
            builder.Entity<Account>().HasData(accounts);

            // Random 1 - 100 transactions for all accounts
            int id = 1;
            var transactions = new List<Transaction>();

            Parallel.ForEach(accounts, account =>
            {
                var numberOfTransactions = new Faker().Random.Number(1, 10);
                var accountTransactions = Enumerable.Range(1, numberOfTransactions).Select(i =>
                {
                    var transaction = new Transaction
                    {
                        Id = System.Threading.Interlocked.Increment(ref id),
                        Date = DateTime.UtcNow.AddDays(-new Faker().Random.Number(1, 30)), // Transaction dates are in the past for example purposes
                        Amount = (double)new Faker().Finance.Amount(),
                        TransactionTypeId = new Faker().Random.Number(1, 6),
                        BankId = new Faker().Random.Number(1, 17),
                        LocationId = new Faker().Random.Number(1, 17),
                        AccountId = account.AccountNumber,
                        CategoryId = new Faker().Random.Number(1, 12),
                    };
                    return transaction;
                });
                lock (transactions)
                {
                    transactions.AddRange(accountTransactions);
                }
            });
            builder.Entity<Transaction>().HasData(transactions);
        }
    }
}
