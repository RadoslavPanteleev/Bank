using BankServer.Controllers.Base;
using BankServer.Controllers.Models;
using BankServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BankController : BankControllerBase<Bank, BankInputModel>
{
    private readonly BankContext bankContext;

    public BankController(BankContext _bankContext) : base(_bankContext.Banks, _bankContext)
    {
        bankContext = _bankContext;
    }

    protected override async Task<Bank> GetRecord(int id)
    {
        return await bankContext.Banks.Include(x => x.PhoneNumber).SingleOrDefaultAsync(x => x.ID == id);
    }

    public override async Task<ActionResult<IList<Bank>>> GetAll()
    {
        return await bankContext.Banks.Include(x => x.PhoneNumber).ToListAsync();
    }

    protected override int GetID(Bank record)
    {
        return record.ID;
    }

    protected override async Task<Bank> GetRecord(BankInputModel source)
    {
        var bank = new Bank {
            UpdateCounter = source.UpdateCounter, 
            Name = source.Name,
            Address = source.Address,
            PhoneNumber = await bankContext.PhoneNumbers.FindAsync(source.PhoneNumberId)
        };

        if (bank.PhoneNumber is null)
            throw new KeyNotFoundException($"PhoneNumberId '{source.PhoneNumberId}' not found!");

        return bank;
    }

    protected override void UpdateRecord(Bank destination, Bank source)
    {
        destination.UpdateCounter = source.UpdateCounter;
        destination.Address = source.Address;
        destination.PhoneNumber = source.PhoneNumber;
        destination.Name = source.Name;
    }
}
