using BankServer.Controllers.Base;
using BankServer.Entities;
using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionTypesController : BankControllerBase<TransactionType, TransactionType, TransactionTypesService>
    {
        private readonly TransactionTypesService transactionTypesService;

        public TransactionTypesController(TransactionTypesService transactionTypesService) : base(transactionTypesService)
        {
            this.transactionTypesService = transactionTypesService;
        }

        [Authorize(Roles = UserRoles.Admin)]
        public override Task<ActionResult> CreateAsync([FromBody] TransactionType inputModel)
        {
            return base.CreateAsync(inputModel);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public override Task<ActionResult> Update([SwaggerParameter(null, Required = true)] TransactionType inputModel, [SwaggerParameter("specific id", Required = true)] int id)
        {
            return base.Update(inputModel, id);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public override Task<ActionResult> Delete([SwaggerParameter("specific id", Required = true)] int id)
        {
            return base.Delete(id);
        }
    }
}
