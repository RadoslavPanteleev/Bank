using BankServer.Controllers.Base;
using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : BankControllerBase<Address, Address, AddressesService>
    {
        private readonly AddressesService addressesService;

        public AddressesController(AddressesService addressesService) : base(addressesService)
        {
            this.addressesService = addressesService;
        }
    }
}
