using BankServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UsersService usersService;

        public UsersController(UsersService _usersService)
        {
            this.usersService = _usersService;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<bool>> Get([Required] string userName)
        {
            if(string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            return await usersService.IsUserNameExists(userName);
        }
    }
}
