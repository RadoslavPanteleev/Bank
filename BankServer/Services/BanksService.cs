using BankServer.Entities;
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
            return await bankContext.Banks.SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IList<Bank>> GetAll()
        {
            return await bankContext.Banks.ToListAsync();
        }

        public override int GetID(Bank record)
        {
            return record.Id;
        }

        protected override Task<Bank> GetRecord(BankInputModel source)
        {
            var bank = new Bank
            {
                Name = source.Name,
                Address = source.Address,
                Phone = source.PhoneNumber
            };

            return Task.FromResult(bank);
        }

        protected override void UpdateRecord(Bank destination, Bank source)
        {
            destination.Address = source.Address;
            destination.Phone = source.Phone;
            destination.Name = source.Name;
        }
    }
}
