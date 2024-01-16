using BankServer.Controllers.Base;
using BankServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneNumbersController : BankControllerBase<PhoneNumber, PhoneNumber>
    {
        private readonly BankContext bankContext;

        public PhoneNumbersController(BankContext _bankContext) : base(_bankContext.PhoneNumbers, _bankContext)
        {
            bankContext = _bankContext;

        }
        protected override int GetID(PhoneNumber record)
        {
            return record.Id;
        }

        protected override Task<PhoneNumber> GetRecord(PhoneNumber source)
        {
            return Task.FromResult(source);
        }

        protected override void UpdateRecord(PhoneNumber destination, PhoneNumber source)
        {
            destination.Phone = source.Phone;
        }
    }
}
