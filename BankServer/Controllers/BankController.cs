using BankServer.Controllers.Base;
using BankServer.Controllers.Models;
using BankServer.Models;
using BankServer.Services;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BankServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankController : BankControllerBase<Bank, BankInputModel, BanksService>
{
    private readonly BanksService banksService;

    public BankController(BanksService banksService) : base(banksService)
    {
        this.banksService = banksService;
    }

    [Authorize(Roles = UserRoles.Admin)]
    public override Task<ActionResult> CreateAsync([FromBody] BankInputModel inputModel)
    {
        return base.CreateAsync(inputModel);
    }

    [Authorize(Roles = UserRoles.Admin)]
    public override Task<ActionResult> Update([SwaggerParameter(null, Required = true)] BankInputModel inputModel, [SwaggerParameter("specific id", Required = true)] int id)
    {
        return base.Update(inputModel, id);
    }

    [Authorize(Roles = UserRoles.Admin)]
    public override Task<ActionResult> Delete([SwaggerParameter("specific id", Required = true)] int id)
    {
        return base.Delete(id);
    }
}
