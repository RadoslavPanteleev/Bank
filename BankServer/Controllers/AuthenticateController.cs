using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly AuthenticateService authenticateService;

        public AuthenticateController(
            AuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] Login model)
        {
            var result = await authenticateService.Login(model);
            if(result is null)
                return Unauthorized();

            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                await authenticateService.Register(model);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            try
            {
                await authenticateService.RegisterAdmin(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
