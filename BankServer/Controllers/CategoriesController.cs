using BankServer.Controllers.Base;
using BankServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : BankControllerBase<Category, Category>
    {
        private readonly BankContext bankContext;

        public CategoriesController(BankContext bankContext) : base(bankContext.Categories, bankContext)
        {
            this.bankContext = bankContext;

        }
        protected override int GetID(Category record)
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
