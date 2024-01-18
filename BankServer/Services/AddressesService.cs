using BankServer.Models;
using BankServer.Services.Base;

namespace BankServer.Services
{
    public class AddressesService : BaseService<Address, Address>
    {
        private readonly BankContext bankContext;
        public AddressesService(BankContext bankContext, ILogger<Address> logger) : base(bankContext.Addresses, bankContext, logger)
        {
            this.bankContext = bankContext;
        }

        public override int GetID(Address record)
        {
            return record.Id;
        }

        protected override Task<Address> GetRecord(Address source)
        {
            return Task.FromResult(source);
        }

        protected override void UpdateRecord(Address destination, Address source)
        {
            destination.Description = source.Description;
        }
    }
}
