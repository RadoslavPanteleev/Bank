using BankServer.Controllers.Base;
using BankServer.Entities;
using BankServer.Models;
using BankServer.Models.Reports;
using BankServer.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Claims;

namespace BankServer.Controllers.Reports
{
    [Authorize(Roles = $"{UserRoles.User}, {UserRoles.Admin}")]
    [ApiController]
    [Route("api/Reports/[controller]")]
    public class TransactionsByBankReportController : ControllerBase
    {
        private readonly TransactionsByBankReportService transactionsByBankReportService;
        private readonly UserManager<Person> userManager;
        private readonly ILogger logger;

        public TransactionsByBankReportController(TransactionsByBankReportService transactionsByBankReportService, UserManager<Person> userManager, ILogger<TransactionsByBankReportController> logger)
        {
            this.transactionsByBankReportService = transactionsByBankReportService;
            this.userManager = userManager;
            this.logger = logger;
        }

        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [HttpGet("GetReport")]
        public async Task<ActionResult<IList<TransactionsByBankNameModel>>> GetReport(string? bankName, string sortBy = "date", bool isDescending = false)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound();

            try
            {
                var result = await transactionsByBankReportService.GetReportByBankName(bankName, person.Id, sortBy, isDescending);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [HttpGet("GetPDFReport")]
        public async Task<HttpResponseMessage> GetPDFReport(string? bankName, string sortBy = "date", bool isDescending = false)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            try
            {
                var pdfFile = await transactionsByBankReportService.GetPDFReportByBankName(bankName, person.Id, sortBy, isDescending);
                var result = await FileHttpResponseMessageHelper.GetMessageWithFileContent(pdfFile);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [HttpGet("GetExcelReport")]
        public async Task<HttpResponseMessage> GetExcelReport(string? bankName, string sortBy = "date", bool isDescending = false)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            try
            {
                var pdfFile = await transactionsByBankReportService.GetExcelReportByBankName(bankName, person.Id, sortBy, isDescending);
                var result = await FileHttpResponseMessageHelper.GetMessageWithFileContent(pdfFile);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
