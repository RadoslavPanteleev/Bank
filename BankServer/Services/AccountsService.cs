using BankServer.Controllers.Models;
using BankServer.Models;
using BankServer.Services.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BankServer.Services
{
    public class AccountsService
    {
        private readonly BankContext bankContext;

        public AccountsService(BankContext bankContext)
        {
            this.bankContext = bankContext;
        }

        public async Task<IList<Account>> GetAll(string personId)
        {
            return await bankContext.Accounts.Include(x => x.Person).Where(x => x.Person!.Id.Equals(personId)).ToListAsync();
        }


        public async Task<Account?> GetRecord(string accountNumber, string personId)
        {
            return await bankContext.Accounts.Include(x => x.Person).Where(x => x.Person!.Id.Equals(personId)).SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);
        }

        public async Task<Account?> CreateAccount(AccountInputModel inputModel, string personId)
        {
            var strategy = bankContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = await bankContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
                {
                    var person = await bankContext.Peoples.SingleOrDefaultAsync(x => x.Id == personId);
                    if (person is null)
                        return null;

                    var account = new Account
                    {
                        UpdateCounter = 0,
                        AccountNumber = Guid.NewGuid().ToString(),
                        AccountName = inputModel.AccountName,
                        Person = person
                    };

                    await bankContext.Accounts.AddAsync(account);
                    await bankContext.SaveChangesAsync();

                    await dbContextTransaction.CommitAsync();

                    return account;
                }
            });
        }

        public async Task<Account> UpdateAccount(AccountInputModel inputModel, string personId)
        {
            var strategy = bankContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = await bankContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
                {
                    var person = await bankContext.Peoples.SingleOrDefaultAsync(x => x.Id == personId);
                    if (person is null)
                        throw new NullReferenceException(personId);

                    var account = await bankContext.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == inputModel.AccountNumber);
                    if (account is null)
                        throw new NullReferenceException("AccountInputModel");

                    if (account.UpdateCounter != inputModel.UpdateCounter)
                        throw new DbUpdateConcurrencyException();

                    account.AccountName = inputModel.AccountName;
                    account.UpdateCounter++;

                    await bankContext.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();

                    return account;
                }
            });
        }

        public async Task DeleteAccount(string accountNumber)
        {
            var strategy = bankContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = await bankContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
                {
                    var account = await bankContext.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);
                    if (account is null)
                        throw new NullReferenceException(accountNumber);

                    bankContext.Accounts.Remove(account);
                    await bankContext.SaveChangesAsync();

                    await dbContextTransaction.CommitAsync();
                }
            });
        }
    }
}
