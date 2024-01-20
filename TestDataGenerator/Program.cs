using BankServer;
using Microsoft.EntityFrameworkCore;
using TestDataGenerator;


char answer = ' ';
do
{
    Console.Write("Are you sure you want to generate fake test data (y/n): ");
    answer = (char)Console.Read();

    if (answer == 'n' || answer == 'N')
        Environment.Exit(0);
}
while (answer != 'y' && answer != 'Y');

Console.WriteLine("Generating...");

using (var context = new AppDbContextTest(new DbContextOptions<AppDbContext>()))
{
    context?.Database.EnsureCreated();
}
