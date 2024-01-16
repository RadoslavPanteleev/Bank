using BankServer.Controllers.Base;
using BankServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionTypesController : BankControllerBase<TransactionType>
    {
        private readonly BankContext bankContext;

        public TransactionTypesController(BankContext bankContext) : base(bankContext.TransactionsTypes, bankContext)
        {
            this.bankContext = bankContext;
        }

        protected override bool CompareByID(TransactionType record, int Id)
        {
            return record.ID == Id;
        }

        protected override int GetID(TransactionType record)
        {
            return record.ID;
        }

        protected override void UpdateRecord(TransactionType destination, TransactionType source)
        {
            destination.Type = source.Type;
        }
    }
}
