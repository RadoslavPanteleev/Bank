using BankServer.Controllers.Base;
using BankServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionTypesController : BankControllerBase<TransactionType, TransactionType>
    {
        private readonly BankContext bankContext;

        public TransactionTypesController(BankContext bankContext) : base(bankContext.TransactionsTypes, bankContext)
        {
            this.bankContext = bankContext;
        }

        protected override int GetID(TransactionType record)
        {
            return record.ID;
        }

        protected override Task<TransactionType> GetRecord(TransactionType source)
        {
            return Task.FromResult(source);
        }

        protected override void UpdateRecord(TransactionType destination, TransactionType source)
        {
            destination.Type = source.Type;
        }
    }
}
