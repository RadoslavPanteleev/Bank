using BankServer.Controllers.Base;
using BankServer.Controllers.Models;
using BankServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : BankControllerBase<Account, AccountInputModel>
    {
        private readonly BankContext bankContext;

        public AccountsController(BankContext bankContext) : base(bankContext.Accounts, bankContext)
        {
            this.bankContext = bankContext;
        }

        protected override async Task<Account?> GetRecord(int id)
        {
            return await bankContext.Accounts.Include(x => x.Person).SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<ActionResult<IList<Account>>> GetAll()
        {
            return await bankContext.Accounts.Include(x => x.Person).ToListAsync();
        }

        protected override int GetID(Account record)
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
    }
}
