using BankServer.Controllers.Models;
using BankServer.Models;
using BankServer.Services;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace BankServer.Controllers
{
    [Authorize(Roles = $"{UserRoles.User}, {UserRoles.Admin}")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly AccountsService accountsService;
        private readonly UserManager<Person> userManager;

        public AccountsController(AccountsService accountsService, UserManager<Person> userManager)
        {
            this.accountsService = accountsService;
            this.userManager = userManager;
        }

        [SwaggerOperation(Summary = "Get all accounts for logged user.")]
        [HttpGet]
        public virtual async Task<ActionResult<IList<Account>>> GetAllAsync()
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound();

            return Ok(await accountsService.GetAll(person.Id));
        }

        [SwaggerOperation(Summary = "Get account for logged user.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You must be with admin role to use this resource!")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "if not found")]
        [HttpGet("{accountNumber}")]
        public virtual async Task<ActionResult<Account?>> GetByAccountNumber([SwaggerParameter("specific accountNumber", Required = true)] string accountNumber)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound(person);

            var record = await accountsService.GetRecord(accountNumber, person.Id);
            if (record is null)
                return NotFound(record);

            return record;
        }

        [SwaggerOperation(Summary = "Create new account")]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "if created")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You must be with admin role to use this resource!")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "if error occured")]
        [HttpPost]
        public virtual async Task<ActionResult<Account>> CreateAccount([FromBody] AccountInputModel inputModel)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound(person);

            try
            {
                var account = await accountsService.CreateAccount(inputModel, person.Id);
                if (account is null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Please check if account number is unique!" });

                return account;
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [SwaggerOperation(Summary = "Update account name only")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if updated")]
        [SwaggerResponse(StatusCodes.Status304NotModified, Description = "if record is already updated from another instance. Please read the latest version of record before edit.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You must be with admin role to use this resource!")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "if error occured")]
        [HttpPut]
        public virtual async Task<ActionResult<Account>> UpdateAccount([FromBody] AccountInputModel inputModel)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound(person);

            try
            {
                var account = await accountsService.UpdateAccount(inputModel, person.Id);
                return account;
            }
            catch(DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status304NotModified, new Response { Status = "Error", Message = "Please read the latest version of record before edit." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [SwaggerOperation(Summary = "Delete record by specific id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found and deleted")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You must be with admin role to use this resource!")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "if not found or other server internal error")]
        [HttpDelete("{accountNumber}")]
        public virtual async Task<ActionResult> Delete([SwaggerParameter("specific id", Required = true)] string accountNumber)
        {
            try
            {
                await accountsService.DeleteAccount(accountNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
