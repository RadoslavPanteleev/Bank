using BankServer.Entities;
using BankServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [Authorize(Roles = $"{UserRoles.User}, {UserRoles.Admin}")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<Person> userManager;
    }
}
