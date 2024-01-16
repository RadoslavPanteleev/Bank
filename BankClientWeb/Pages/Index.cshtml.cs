using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace BankClientWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        public IActionResult OnGet()
        {
            string? cookie = Request.Cookies["Token"];
            if (string.IsNullOrEmpty(cookie))
            {
                return Redirect("/Login");
            }

            return new OkResult();
        }
    }
}
