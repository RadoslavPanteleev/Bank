﻿using BankServer.Entities;
using BankServer.Models;
using BankServer.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Services
{
    public class BanksService : BaseService<Bank, BankInputModel>
    {
        private readonly AppDbContext bankContext;
        public BanksService(AppDbContext bankContext, ILogger<Bank> logger) : base(bankContext.Banks, bankContext, logger)
        {
            this.bankContext = bankContext;
        }

        public override async Task<Bank?> GetRecord(int id)
        {
            return await bankContext.Banks.Include(x => x.PhoneNumber).SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IList<Bank>> GetAll()
        {
            return await bankContext.Banks.Include(x => x.PhoneNumber).ToListAsync();
        }

        public override int GetID(Bank record)
        {
            return record.Id;
        }

        protected override async Task<Bank> GetRecord(BankInputModel source)
        {
            var bank = new Bank
            {
                Name = source.Name,
                Address = source.Address,
                PhoneNumber = await bankContext.PhoneNumbers.FindAsync(source.PhoneNumberId)
            };

            if (bank.PhoneNumber is null)
                throw new KeyNotFoundException($"PhoneNumberId '{source.PhoneNumberId}' not found!");

            return bank;
        }

        protected override void UpdateRecord(Bank destination, Bank source)
        {
            destination.Address = source.Address;
            destination.PhoneNumber = source.PhoneNumber;
            destination.Name = source.Name;
        }
    }
}
