using BankServer.Entities;
using BankServer.Services.Base;

namespace BankServer.Services
{
    public class AddressesService : RepositoryBaseService<Address, Address>
    {
        private readonly AppDbContext bankContext;
        public AddressesService(AppDbContext bankContext, ILogger<Address> logger) : base(bankContext, logger)
        {
            this.bankContext = bankContext;
        }

        public override int GetID(Address record)
        {
            return record.Id;
        }

        protected override Task<Address> MapModel(Address source)
        {
            return Task.FromResult(source);
        }

        protected override void CopyValues(Address destination, Address source)
        {
            destination.Description = source.Description;
        }
    }
}
