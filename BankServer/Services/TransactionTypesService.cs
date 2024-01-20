using BankServer.Entities;
using BankServer.Services.Base;

namespace BankServer.Services
{
    public class TransactionTypesService : RepositoryBaseService<TransactionType, TransactionType>
    {
        private readonly AppDbContext bankContext;

        public TransactionTypesService(AppDbContext bankContext, ILogger<TransactionType> logger) : base(bankContext, logger)
        {
            this.bankContext = bankContext;
        }

        public override int GetID(TransactionType record)
        {
            return record.Id;
        }

        protected override Task<TransactionType> MapModel(TransactionType source)
        {
            return Task.FromResult(source);
        }

        protected override void CopyValues(TransactionType destination, TransactionType source)
        {
            destination.Type = source.Type;
        }
    }
}
