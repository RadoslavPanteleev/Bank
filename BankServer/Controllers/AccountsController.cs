using BankServer.Controllers.Base;
using BankServer.Controllers.Models;
using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : BankControllerBase<Account, AccountInputModel, AccountsService>
    {
        private readonly AccountsService accountsService;

        public AccountsController(AccountsService accountsService) : base(accountsService)
        {
            this.accountsService = accountsService;
        }
    }
}
