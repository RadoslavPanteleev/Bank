using BankServer.Entities;
using BankServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankServer.Services
{
    public class AuthenticateService
    {
        private readonly UserManager<Person> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateService(UserManager<Person> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<object?> Login(Login model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user is null)
                return null;

            if(!await userManager.CheckPasswordAsync(user, model.Password))
                return null;

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

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }

        public async Task Register(RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                throw new Exception("User already exists!");

            Person user = new Person()
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Phone = model.Phone,
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new Exception("User creation failed! Please check user details and try again.");
        }

        public async Task RegisterAdmin(RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                throw new Exception("User already exists!");

            var admins = await userManager.GetUsersInRoleAsync(UserRoles.Admin);
            if (admins.Count > 1)
                throw new Exception("There is already registered admin!");

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
                Phone = model.Phone,
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new Exception("User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
        }
    }
}
