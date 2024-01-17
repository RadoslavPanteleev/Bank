using BankServer.Controllers.Base;
using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneNumbersController : BankControllerBase<PhoneNumber, PhoneNumber, PhoneNumbersService>
    {
        private readonly PhoneNumbersService phoneNumbersService;

        public PhoneNumbersController(PhoneNumbersService phoneNumbersService) : base(phoneNumbersService)
        {
            this.phoneNumbersService = phoneNumbersService;
        }
    }
}
