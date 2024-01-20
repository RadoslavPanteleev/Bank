using BankServer.Entities;
using BankServer.Services.Base;

namespace BankServer.Services
{
    public class CategoriesService : RepositoryBaseService<Category, Category>
    {
        private readonly AppDbContext bankContext;
        public CategoriesService(AppDbContext bankContext, ILogger<Category> logger) : base(bankContext, logger)
        {
            this.bankContext = bankContext;
        }

        public override int GetID(Category record)
        {
            return record.Id;
        }

        protected override Task<Category> MapModel(Category source)
        {
            return Task.FromResult(source);
        }

        protected override void CopyValues(Category destination, Category source)
        {
            destination.Description = source.Description;
        }
    }
}
