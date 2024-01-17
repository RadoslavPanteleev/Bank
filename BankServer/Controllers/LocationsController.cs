using BankServer.Controllers.Base;
using BankServer.Controllers.Models;
using BankServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : BankControllerBase<Location, LocationInputModel>
    {
        private readonly BankContext bankContext;

        public LocationsController(BankContext bankContext) : base(bankContext.Locations, bankContext)
        {
            this.bankContext = bankContext;
        }

        protected override async Task<Location?> GetRecord(int id)
        {
            return await bankContext.Locations.Include(x => x.Address).SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<ActionResult<IList<Location>>> GetAll()
        {
            return await bankContext.Locations.Include(x => x.Address).ToListAsync();
        }

        protected override int GetID(Location record)
        {
            return record.Id;
        }

        protected override async Task<Location> GetRecord(LocationInputModel source)
        {
            var location = new Location
            {
                UpdateCounter = source.UpdateCounter,
                Name = source.Name,
                Address = await bankContext.Addresses.FindAsync(source.AddressId),
                Longitude = source.Longitude,
                Latitude = source.Latitude
            };

            if (location.Address is null && source.AddressId != 0)
                throw new KeyNotFoundException($"AddressId {source.AddressId} not found!");

            return location;
        }

        protected override void UpdateRecord(Location destination, Location source)
        {
            destination.Address = source.Address;
            destination.Latitude = source.Latitude;
            destination.Longitude = source.Longitude;
            destination.Name = source.Name;
        }
    }
}
