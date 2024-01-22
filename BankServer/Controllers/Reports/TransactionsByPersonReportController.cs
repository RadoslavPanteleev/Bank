using BankServer.Controllers.Base;
using BankServer.Models;
using BankServer.Models.Reports;
using BankServer.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BankServer.Controllers.Reports
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/Reports/[controller]")]
    public class TransactionsByPersonReportController : ControllerBase
    {
        private readonly TransactionsByPersonReportService transactionsByPersonReportService;
        private readonly ILogger logger;

        public TransactionsByPersonReportController(TransactionsByPersonReportService transactionsByPersonReportService, ILogger<TransactionsByLocationReportController> logger)
        {
            this.transactionsByPersonReportService = transactionsByPersonReportService;
            this.logger = logger;
        }

        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [HttpGet("GetReport")]
        public async Task<ActionResult<IList<TransactionsByLocationNameModel>>> GetReport(string? personUserName, string? personFirstName, string? personLastName, string? personId, string sortBy = "date", bool isDescending = false)
        {
            try
            {
                var result = await transactionsByPersonReportService.GetReportByPersonName(personUserName, personFirstName, personLastName, personId, sortBy, isDescending);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [HttpGet("GetPDFReport")]
        public async Task<HttpResponseMessage> GetPDFReport(string? personUserName, string? personFirstName, string? personLastName, string? personId, string sortBy = "date", bool isDescending = false)
        {
            try
            {
                var pdfFile = await transactionsByPersonReportService.GetPDFReportByPersonName(personUserName, personFirstName, personLastName, personId, sortBy, isDescending);
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
        public async Task<HttpResponseMessage> GetExcelReport(string? personUserName, string? personFirstName, string? personLastName, string? personId, string sortBy = "date", bool isDescending = false)
        {
            try
            {
                var pdfFile = await transactionsByPersonReportService.GetExcelReportByPersonName(personUserName, personFirstName, personLastName, personId, sortBy, isDescending);
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
