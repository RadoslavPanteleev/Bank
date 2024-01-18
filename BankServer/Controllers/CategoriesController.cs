using BankServer.Controllers.Base;
using BankServer.Entities;
using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : BankControllerBase<Category, Category, CategoriesService>
    {
        private readonly CategoriesService categoriesService;

        public CategoriesController(CategoriesService categoriesService) : base(categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [Authorize(Roles = UserRoles.Admin)]
        public override Task<ActionResult> CreateAsync([FromBody] Category inputModel)
        {
            return base.CreateAsync(inputModel);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public override Task<ActionResult> Update([SwaggerParameter(null, Required = true)] Category inputModel, [SwaggerParameter("specific id", Required = true)] int id)
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
