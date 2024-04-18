using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SocietySync.Pages
{
    public class UserProfileModel : PageModel
    {
        public void OnGet()
        {
            var context = UserSession.Instance.GetSocietySyncContext();

            string Roll_Number = UserSession.Instance.LoggedInRollNumber;
            ViewData["Username"] = Roll_Number;

            var User = context.Users.FirstOrDefault(u=> u.RollNum == Roll_Number);
            ViewData["Name"] = User?.Name;

        }

        public IActionResult OnPost(string page)
        {
            //Navigate According to what has been clicked by the user
            return page switch
            {
                "RegisterSociety" => RedirectToPage("/SocietyRegisteration"),
                _=> RedirectToPage("/Index"), 
            };
        }


    }
}
    