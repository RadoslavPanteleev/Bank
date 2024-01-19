using BankServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers.Reports
{
    [Authorize(Roles = $"{UserRoles.User}, {UserRoles.Admin}")]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsByLocationReportController
    {
        public TransactionsByLocationReportController() 
        { 
            
        }


    }
}
