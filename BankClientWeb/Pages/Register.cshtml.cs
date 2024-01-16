using BankClientWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BankClientWeb.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string? UserName { get; set; }

        [BindProperty]
        public string? FirstName { get; set; }

        [BindProperty]
        public string? LastName { get; set; }

        [BindProperty]
        public string? Address { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [BindProperty]
        public string? RepeatedPassword { get; set; }

        [BindProperty]
        public string? AccountNumber { get; set; }

        [TempData]
        public string StatusMessage { get; set; } = "";

        private readonly UsersService usersService;

        public RegisterModel(UsersService _usersService)
        {
            usersService = _usersService;
        }

        public async Task<IActionResult> OnGet()
        {
            string? cookie = Request.Cookies["Token"];
            if (!string.IsNullOrEmpty(cookie))
            {
                return Redirect("/");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            StatusMessage = "";

            if(!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(RepeatedPassword) && Password.CompareTo(RepeatedPassword) != 0)
            {
                StatusMessage = "Passwords not match!";
                return Page();
            }

            try
            {
                if(!string.IsNullOrEmpty(UserName) && await usersService.IsUserNameExistsAsync(UserName))
                {
                    StatusMessage = "Username already exists!";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Client error!";
                throw;
            }

            return Redirect("/");
        }
    }
}
