using BankServer.Controllers.Base;
using BankServer.Controllers.Models;
using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : BankControllerBase<Location, LocationInputModel, LocationsService>
    {
        private readonly LocationsService locationsService;

        public LocationsController(LocationsService locationsService) : base(locationsService)
        {
            this.locationsService = locationsService;
        }
    }
}
