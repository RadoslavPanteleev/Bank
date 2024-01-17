using BankServer.Controllers.Models;
using BankServer.Models;
using BankServer.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Services
{
    public class AccountsService : BaseService<Account, AccountInputModel>
    {
        private readonly BankContext bankContext;

        public AccountsService(BankContext bankContext) : base(bankContext.Accounts, bankContext)
        {
            this.bankContext = bankContext;
        }

        #region Overrides

        public override async Task<Account?> GetRecord(int id)
        {
            return await bankContext.Accounts.Include(x => x.Person).SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IList<Account>> GetAll()
        {
            return await bankContext.Accounts.Include(x => x.Person).ToListAsync();
        }

        public override int GetID(Account record)
        {
            return record.Id;
        }

        protected override async Task<Account> GetRecord(AccountInputModel source)
        {
            Account account = new Account
            {
                UpdateCounter = source.UpdateCounter,
                AccountNumber = source.AccountNumber,
                Person = await bankContext.Peoples.FindAsync(source.PersonID)
            };

            if (account.Person is null)
                throw new KeyNotFoundException($"PersonID {source.PersonID} not found!");

            return account;
        }

        protected override void UpdateRecord(Account destination, Account source)
        {
            destination.AccountNumber = source.AccountNumber;
            destination.Person = source.Person;
        }

        #endregion
    }
}
