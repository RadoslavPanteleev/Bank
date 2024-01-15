using BankServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BankController
{
    private readonly BankContext bankContext;

    public BankController(BankContext _bankContext)
    {
        bankContext = _bankContext;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Bank>>> GetAll()
    {
        return await bankContext.Banks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Bank>> Get(int id)
    {
        var bank = await bankContext.Banks.FirstOrDefaultAsync(x => x.ID == id);
        if (bank is null)
            return new NotFoundResult();

        return bank;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Bank bank)
    {
        await bankContext.Banks.AddAsync(bank);
        await bankContext.SaveChangesAsync();

        return new OkResult();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Bank bank)
    {
        if (id != bank.ID)
            return new BadRequestResult();

        return null;
    }
}
