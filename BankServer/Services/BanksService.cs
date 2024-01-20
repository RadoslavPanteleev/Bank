using BankServer.Entities;
using BankServer.Models;
using BankServer.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Services
{
    public class BanksService : RepositoryBaseService<Bank, BankInputModel>
    {
        private readonly AppDbContext bankContext;
        public BanksService(AppDbContext bankContext, ILogger<Bank> logger) : base(bankContext, logger)
        {
            this.bankContext = bankContext;
        }

        public override async Task<Bank?> GetRecordAsync(int id)
        {
            return await bankContext.Banks.SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IList<Bank>> GetAllAsync()
        {
            return await bankContext.Banks.ToListAsync();
        }

        public override int GetID(Bank record)
        {
            return record.Id;
        }

        protected override Task<Bank> MapModel(BankInputModel source)
        {
            var bank = new Bank
            {
                Name = source.Name,
                Address = source.Address,
                Phone = source.PhoneNumber
            };

            return Task.FromResult(bank);
        }

        protected override void CopyValues(Bank destination, Bank source)
        {
            destination.Address = source.Address;
            destination.Phone = source.Phone;
            destination.Name = source.Name;
        }
    }
}
