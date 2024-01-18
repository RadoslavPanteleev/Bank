﻿using BankServer.Entities;
using BankServer.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Services
{
    public class PhoneNumbersService : BaseService<PhoneNumber, PhoneNumber>
    {
        private readonly AppDbContext bankContext;

        public PhoneNumbersService(AppDbContext _bankContext, ILogger<PhoneNumber> logger) : base(_bankContext.PhoneNumbers, _bankContext, logger)
        {
            bankContext = _bankContext;
        }

        public override int GetID(PhoneNumber record)
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

        public async Task<PhoneNumber?> GetPhoneNumber(string? number) 
        {
            if (string.IsNullOrEmpty(number))
                return null;

            return await bankContext.PhoneNumbers.FirstOrDefaultAsync(x => x.Phone == number);
        }
    }
}
