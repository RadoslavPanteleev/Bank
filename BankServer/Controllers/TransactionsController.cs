using BankServer.Entities;
using BankServer.Models;
using BankServer.Services;
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
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsService transactionsService;
        private readonly UserManager<Person> userManager;

        public TransactionsController(TransactionsService transactionsService, UserManager<Person> userManager)
        {
            this.transactionsService = transactionsService;
            this.userManager = userManager;
        }

        [SwaggerOperation(Summary = "Get all transactions for logged user.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [HttpGet]
        public virtual async Task<ActionResult<IList<TransactionOutputModel>>> GetAllAsync()
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound();

            return Ok(await transactionsService.GetAllAsync(person.Id));
        }

        [SwaggerOperation(Summary = "Get transaction for logged user.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You must be with admin role to use this resource!")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "if not found")]
        [HttpGet("{transactionId}")]
        public virtual async Task<ActionResult<TransactionOutputModel?>> GetByTransactionId([SwaggerParameter("specific id", Required = true)] int transactionId)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound(person);

            var record = await transactionsService.GetTransactionByIdAsync(transactionId, person.Id);
            if (record is null)
                return NotFound(record);

            return record;
        }

        [SwaggerOperation(Summary = "Create new transaction")]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "if created")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You must be with admin role to use this resource!")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "if error occured")]
        [HttpPost]
        public virtual async Task<ActionResult<Transaction>> CreateTransaction([FromBody] AddTransactionModel addTransactionModel)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound(person);

            try
            {
                var transaction = await transactionsService.CreateTransactionAsync(person.Id, addTransactionModel);
                return transaction;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [SwaggerOperation(Summary = "Update transaction")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if updated")]
        [SwaggerResponse(StatusCodes.Status304NotModified, Description = "if record is already updated from another instance. Please read the latest version of record before edit.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You must be with admin role to use this resource!")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "if error occured")]
        [HttpPut("{transactionId}")]
        public virtual async Task<ActionResult<Transaction?>> UpdateAccount([SwaggerParameter("specific id", Required = true)] int transactionId, [FromBody] AddTransactionModel addTransactionModel)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound(person);

            try
            {
                var transaction = await transactionsService.UpdateTransactionAsync(person.Id, transactionId, addTransactionModel);
                return transaction;
            }
            catch (DbUpdateConcurrencyException)
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
        [HttpDelete("{transactionId}")]
        public virtual async Task<ActionResult> Delete([SwaggerParameter("specific id", Required = true)] int transactionId)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound(person);

            try
            {
                await transactionsService.DeleteTransaction(person.Id, transactionId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
