using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocietySync.Models;

namespace SocietySync.Pages
{
    public class SocietyProfileModel : PageModel
    {

        public List<User> pending_Users; 
        public void OnGet()
        {

        }

        public void ManageRequests_ButtonClick()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            string My_SocietyName = Request.Query["stringValue"];

                pending_Users = context.SocietyMemberships
                .Where(sm => sm.Society_Name == My_SocietyName && sm.Role == "New_member")
                .Select(sm => sm.User) 
                .ToList();

            ViewData["ManageRequestsPopup"] = true;
        }

        public void viewRequestDetails_ButtonClick()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (Request.Form.TryGetValue("MenuButton", out var MenuButton))
            {
                switch (MenuButton)
                {
                    case "ManageRequests":
                        ManageRequests_ButtonClick();
                        break;
                        /* case "CurrentSocieties":
                             CurrentSocieties_buttonClick();
                             break;
                         case "Register":
                             Register_buttonClick();
                             break;
                         case "MySocietiesBtn":
                             MySocieties_buttonClick();
                             break;*/
                }
            }


            if (Request.Form.TryGetValue("viewRequestDetailsBtn", out var viewRequestDetailsBtn))
            {
                viewRequestDetails_ButtonClick();
            }

            return Page();
        }
    }
}
