using BankServer.Controllers.Base;
using BankServer.Models;
using BankServer.Services;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionTypesController : BankControllerBase<TransactionType, TransactionType, TransactionTypesService>
    {
        private readonly TransactionTypesService transactionTypesService;

        public TransactionTypesController(TransactionTypesService transactionTypesService) : base(transactionTypesService)
        {
            this.transactionTypesService = transactionTypesService;
        }
    }
}
