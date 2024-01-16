using BankServer.Controllers.Base;
using BankServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : BankControllerBase<Address, Address>
    {
        private readonly BankContext bankContext;

        public AddressesController(BankContext bankContext) : base(bankContext.Addresses, bankContext)
        {
            this.bankContext = bankContext;
        }

        protected override int GetID(Address record)
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
