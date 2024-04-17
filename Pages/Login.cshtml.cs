using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SocietySync.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username_input { get; set; }

        [BindProperty]
        public string Password_input { get; set; }

        [BindProperty]
        public bool ShowPassword { get; set; }

        public void OnGet()
        {
            ShowPassword = false;
        }

        public IActionResult OnPost()
        {
            UserSession userSession = UserSession.Instance;
            userSession.SetLoggedInUser(Username_input);

            var context = userSession.GetSocietySyncContext();

            var user = context.Users.FirstOrDefault(u => u.RollNum == Username_input);

            if (user == null)
            {
                ModelState.AddModelError(nameof(LoginModel.Username_input), "The Roll Number you entered is not signed in Society Sync");
                return Page();
            }

            if (user.Hashed_Password != Password_Hasher.HashPassword(Password_input))
            {
                ModelState.AddModelError(nameof(LoginModel.Password_input), "The Password you entered is incorrect");
                return Page();
            }

            return RedirectToPage("/Index");
        }

    }
}
