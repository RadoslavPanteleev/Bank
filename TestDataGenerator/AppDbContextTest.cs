using BankServer;
using Microsoft.EntityFrameworkCore;

namespace TestDataGenerator
{
    public partial class AppDbContextTest : AppDbContext
    {
        public AppDbContextTest(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            GenerateFakeTestData(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-T80Q55G\\SQLEXPRESS; Initial Catalog=BANK; User Id=sa; Password=1234");
        }
    }
}
