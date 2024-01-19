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
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService profileService;
        private readonly UserManager<Person> userManager;

        public ProfileController(ProfileService profileService, UserManager<Person> userManager)
        {
            this.profileService = profileService;
            this.userManager = userManager;
        }

        [SwaggerOperation(Summary = "Get profile info for logged user.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You must be with admin role to use this resource!")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "if not found")]
        [HttpGet(nameof(GetProfileInfo))]
        public virtual async Task<ActionResult<GetProfileInfoResponse?>> GetProfileInfo()
        {
            var record = await profileService.GetProfileInfo(User.FindFirstValue(ClaimTypes.Name));
            if (record is null)
                return NotFound(record);

            return record;
        }

        [SwaggerOperation(Summary = "Edit profile info")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if updated")]
        [SwaggerResponse(StatusCodes.Status304NotModified, Description = "if record is already updated from another instance. Please read the latest version of record before edit.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You must be with admin role to use this resource!")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "if error occured")]
        [HttpPut(nameof(EditProfileInfo))]
        public virtual async Task<ActionResult> EditProfileInfo([FromBody] EditProfileInput editProfileInput)
        {
            try
            {
                await profileService.EditProfileInfo(User.FindFirstValue(ClaimTypes.Name), editProfileInput);
                return Ok();
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

        [SwaggerOperation(Summary = "Edit password")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if updated")]
        [SwaggerResponse(StatusCodes.Status304NotModified, Description = "if record is already updated from another instance. Please read the latest version of record before edit.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "You must be signed to use this resource!")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You must be with admin role to use this resource!")]
        [HttpPut(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword(ChangePasswordInput changePasswordInput)
        {
            try
            {
                await profileService.ChangePassword(User.FindFirstValue(ClaimTypes.Name), changePasswordInput);
                return Ok();
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
    }
}
