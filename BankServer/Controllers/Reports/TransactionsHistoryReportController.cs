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
    public class TransactionsHistoryReportController : ControllerBase
    {
        private readonly TransactionsHistoryReportService transactionsHistoryReportService;
        private readonly UserManager<Person> userManager;
        private readonly ILogger logger;

        public TransactionsHistoryReportController(TransactionsHistoryReportService transactionsHistoryReportService, UserManager<Person> userManager, ILogger<TransactionsByLocationReportController> logger)
        {
            this.transactionsHistoryReportService = transactionsHistoryReportService;
            this.userManager = userManager;
            this.logger = logger;
        }

        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [HttpGet("GetReport")]
        public async Task<ActionResult<IList<TransactionsHistoryModel>>> GetReport(DateTime dateFrom, DateTime dateTo, string sortBy = "date", bool isDescending = false)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return NotFound();

            try
            {
                var result = await transactionsHistoryReportService.GetReportHistory(dateFrom, dateTo, person.Id, sortBy, isDescending);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [HttpGet("GetPDFReport")]
        public async Task<HttpResponseMessage> GetPDFReport(DateTime dateFrom, DateTime dateTo, string sortBy = "date", bool isDescending = false)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            try
            {
                var pdfFile = await transactionsHistoryReportService.GetPDFReportHistory(dateFrom, dateTo, person.Id, sortBy, isDescending);
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
        public async Task<HttpResponseMessage> GetExcelReport(DateTime dateFrom, DateTime dateTo, string sortBy = "date", bool isDescending = false)
        {
            var person = await userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));
            if (person is null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            try
            {
                var pdfFile = await transactionsHistoryReportService.GetExcelReportHistory(dateFrom, dateTo, person.Id, sortBy, isDescending);
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
