using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocietySync.Models;
using SocietySync.DBcontext;
using System.Net.Sockets;

namespace SocietySync.Pages
{
    public class UserProfileModel : PageModel
    {
        public List<Announcement> notifications;

        void populateData()
        {
            var context = UserSession.Instance.GetSocietySyncContext();

            string Roll_Number = UserSession.Instance.LoggedInRollNumber;
            ViewData["Username"] = Roll_Number;

            var User = context.Users.FirstOrDefault(u => u.RollNum == Roll_Number);
            ViewData["Name"] = User?.Name;
        }

        public void OnGet()
        {
            populateData();

        }

        public IActionResult OnPost(string page)
        {
           if (Request.Form.TryGetValue("updateProfileBtn", out var updateProfileBtn))
            {
                var context = UserSession.Instance.GetSocietySyncContext();
                var user = context.Users.FirstOrDefault(u => u.RollNum == UserSession.Instance.LoggedInRollNumber);
                string editedName = Request.Form["Name"];
                user.Name = editedName;
                context.SaveChanges();
                populateData();
                return Page();
            }

            //Navigate According to what has been clicked by the user
            return page switch
            {
                "RegisterSociety" => RedirectToPage("/SocietyRegisteration"),
                "ApplyMembership" => RedirectToPage("/ApplyMembership"),
                "Home" => RedirectToPage("/UserProfile"),
                "Logout" => RedirectToPage("/Index"),
            }; 
        }


    }
}
    