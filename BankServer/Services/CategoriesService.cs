using BankServer.Models;
using BankServer.Services.Base;

namespace BankServer.Services
{
    public class CategoriesService : BaseService<Category, Category>
    {
        private readonly BankContext bankContext;
        public CategoriesService(BankContext bankContext, ILogger<Category> logger) : base(bankContext.Categories, bankContext, logger)
        {
            this.bankContext = bankContext;
        }

        public override int GetID(Category record)
        {
            return record.ID;
        }

        protected override Task<Category> GetRecord(Category source)
        {
            return Task.FromResult(source);
        }

        protected override void UpdateRecord(Category destination, Category source)
        {
            destination.Description = source.Description;
        }
    }
}
