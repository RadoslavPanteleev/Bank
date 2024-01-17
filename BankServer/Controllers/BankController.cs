using BankServer.Controllers.Base;
using BankServer.Controllers.Models;
using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BankController : BankControllerBase<Bank, BankInputModel, BanksService>
{
    private readonly BanksService banksService;

    public BankController(BanksService banksService) : base(banksService)
    {
        this.banksService = banksService;
    }
}
