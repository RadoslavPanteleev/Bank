using BankServer.Models;
using BankServer.Services.Base;

namespace BankServer.Services
{
    public class TransactionTypesService : BaseService<TransactionType, TransactionType>
    {
        private readonly BankContext bankContext;

        public TransactionTypesService(BankContext bankContext) : base(bankContext.TransactionsTypes, bankContext)
        {
            this.bankContext = bankContext;
        }

        public override int GetID(TransactionType record)
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
