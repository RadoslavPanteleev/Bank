﻿using BankServer.Entities;
using BankServer.Models;
using BankServer.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Services
{
    public class LocationsService : RepositoryBaseService<Location, LocationInputModel>
    {
        private readonly AppDbContext bankContext;

        public LocationsService(AppDbContext bankContext, ILogger<Location> logger) : base(bankContext, logger)
        {
            this.bankContext = bankContext;
        }

        public override async Task<Location?> GetRecordAsync(int id)
        {
            return await bankContext.Locations.Include(x => x.Address).SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IList<Location>> GetAllAsync()
        {
            return await bankContext.Locations.Include(x => x.Address).ToListAsync();
        }

        public override int GetID(Location record)
        {
            return record.Id;
        }

        protected override async Task<Location> MapModel(LocationInputModel source)
        {
            var location = new Location
            {
                Name = source.Name,
                Address = await bankContext.Addresses.FindAsync(source.AddressId),
                Longitude = source.Longitude,
                Latitude = source.Latitude
            };

            if (location.Address is null && source.AddressId != 0)
                throw new KeyNotFoundException($"AddressId {source.AddressId} not found!");

            return location;
        }

        protected override void CopyValues(Location destination, Location source)
        {
            destination.Address = source.Address;
            destination.Latitude = source.Latitude;
            destination.Longitude = source.Longitude;
            destination.Name = source.Name;
        }
    }
}
