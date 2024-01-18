using BankServer.Entities;
using BankServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BankServer.Services
{
    public class AccountsService
    {
        private readonly AppDbContext appDbContext;
        private readonly ILogger logger;

        public AccountsService(AppDbContext appDbContext, ILogger<AccountsService> logger)
        {
            this.appDbContext = appDbContext;
            this.logger = logger;
        }

        public async Task<IList<Account>> GetAll(string personId)
        {
            return await appDbContext.Accounts.Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<Account?> GetRecord(Guid accountNumber, string personId)
        {
            return await appDbContext.Accounts.Where(x => x.PersonId == personId).SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);
        }

        public async Task<Account?> CreateAccount(AccountInputModel inputModel, string personId)
        {
            var account = new Account
            {
                AccountNumber = Guid.NewGuid(),
                AccountName = inputModel.AccountName,
                PersonId = personId
            };

            await appDbContext.Accounts.AddAsync(account);
            await appDbContext.SaveChangesAsync();

            return account;
        }

        public async Task<Account?> UpdateAccount(AccountInputModel inputModel, Guid accountNumber)
        {
            var account = await appDbContext.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if (account is null)
                throw new NullReferenceException("AccountInputModel");

            account.AccountName = inputModel.AccountName;

            await appDbContext.SaveChangesAsync();
            return account;
        }

        public async Task DeleteAccount(Guid accountNumber)
        {
            var strategy = appDbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = await appDbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable))
                {
                    try
                    {
                        var account = await appDbContext.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);
                        if (account is null)
                            throw new NullReferenceException(accountNumber.ToString());

                        appDbContext.Accounts.Remove(account);
                        await appDbContext.SaveChangesAsync();

                        await dbContextTransaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await dbContextTransaction.RollbackAsync();
                        this.logger.LogError(ex.Message);
                    }
                }
            });
        }
    }
}
