using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankServer.Entities.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<Person>
    {
        public static readonly string AdminId = "475BAAC2-7B3C-4E9B-BCB7-C9C545EA371C";
        private readonly string defaultPassword = "Test1234.";

        public string PassGenerate(Person user)
        {
            var passHash = new PasswordHasher<Person>();
            return passHash.HashPassword(user, defaultPassword);

        }
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            var user = new Person
            {
                Id = AdminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                FirstName = "Radoslav",
                LastName = "Panteleev",
                Email = "radoslav.m.panteleev@gmail.com",
                NormalizedEmail = "RADOSLAV.M.PANTELEEV@GMAIL.COM",
                Phone = "+359 89 352 5385",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString("D")
            };
            user.PasswordHash = PassGenerate(user);
            builder.HasData(user);
        }
    }
}
