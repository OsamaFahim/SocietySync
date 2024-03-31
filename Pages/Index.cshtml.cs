using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SocietySync.Pages
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
            return Page();
        }

        public IActionResult OnPostLogin()
        {
            // Handle login logic here

            // Redirect to the login page
            return RedirectToPage("/Login");
        }

        public IActionResult OnPostSignup()
        {
            // Handle signup logic here

            // Redirect to the signup page
            return RedirectToPage("/Signup");
        }
    }
}
