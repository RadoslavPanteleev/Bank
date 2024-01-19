using BankServer.Entities;
using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly AppDbContext bankContext;
        private readonly PhoneNumbersService phoneNumberService;
        private readonly UserManager<Person> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(
            AppDbContext bankContext, 
            PhoneNumbersService phoneNumberService, 
            UserManager<Person> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration)
        {
            this.bankContext = bankContext;
            this.phoneNumberService = phoneNumberService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            var phoneNumber = await this.phoneNumberService.GetPhoneNumber(model.Phone);
            if(phoneNumber is null)
            {
                phoneNumber = await this.phoneNumberService.CreateAsync(new PhoneNumber { Phone = model.Phone });
                if(phoneNumber is null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            Person user = new Person()
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = phoneNumber,
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            var admins = await userManager.GetUsersInRoleAsync(UserRoles.Admin);
            if(admins.Count > 1)
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "There is already registered admin!" });

            var phoneNumber = await this.phoneNumberService.GetPhoneNumber(model.Phone);
            if (phoneNumber is null)
            {
                phoneNumber = await this.phoneNumberService.CreateAsync(new PhoneNumber { Phone = model.Phone });
                if (phoneNumber is null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            //var address = await this.addressesService.Get(model.AddressId);
            //if (model.AddressId > 0 && address is null)
            //{
            //    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = $"Address id {model.AddressId} not found!" });
            //}

            Person user = new Person()
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = phoneNumber,
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
