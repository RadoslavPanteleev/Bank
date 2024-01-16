using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankClientWeb.Pages
{
    public class LoginModel : PageModel
    {
        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public void OnGet()
        {
        }
    }
}
