using BankServer.Controllers.Base;
using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
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
