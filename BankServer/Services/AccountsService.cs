using BankServer.Controllers.Models;
using BankServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BankServer.Services
{
    public class AccountsService
    {
        private readonly BankContext bankContext;
        private readonly ILogger logger;


        public AccountsService(BankContext bankContext, ILogger<AccountsService> logger)
        {
            this.bankContext = bankContext;
            this.logger = logger;
        }

        public async Task<IList<Account>> GetAll(string personId)
        {
            return await bankContext.Accounts.Where(x => x.PersonId == personId).ToListAsync();
        }

        public async Task<Account?> GetRecord(Guid accountNumber, string personId)
        {
            return await bankContext.Accounts.Where(x => x.PersonId == personId).SingleOrDefaultAsync(x => x.AccountNumber.Equals(accountNumber));
        }

        public async Task<Account?> CreateAccount(AccountInputModel inputModel, string personId)
        {
            var account = new Account
            {
                AccountNumber = Guid.NewGuid(),
                AccountName = inputModel.AccountName,
                PersonId = personId
            };

            await bankContext.Accounts.AddAsync(account);
            await bankContext.SaveChangesAsync();

            return account;
        }

        public async Task<Account?> UpdateAccount(AccountInputModel inputModel, string personId)
        {
            var strategy = bankContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                var account = await bankContext.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == inputModel.AccountNumber);
                if (account is null)
                    throw new NullReferenceException("AccountInputModel");

                account.AccountName = inputModel.AccountName;

                await bankContext.SaveChangesAsync();
                return account;
            });
        }

        public async Task DeleteAccount(Guid accountNumber)
        {
            var strategy = bankContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = await bankContext.Database.BeginTransactionAsync(IsolationLevel.Serializable))
                {
                    try
                    {
                        var account = await bankContext.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);
                        if (account is null)
                            throw new NullReferenceException(accountNumber.ToString());

                        bankContext.Accounts.Remove(account);
                        await bankContext.SaveChangesAsync();

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
