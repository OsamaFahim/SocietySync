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

        void populate_UserData()
        {
            var context = UserSession.Instance.GetSocietySyncContext();

            string Roll_Number = UserSession.Instance.LoggedInRollNumber;
            ViewData["Username"] = Roll_Number;

            var User = context.Users.FirstOrDefault(u => u.RollNum == Roll_Number);
            ViewData["Name"] = User?.Name;
        }

        public void populate_notifications()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            List<string> user_societies = context.SocietyMemberships
                                      .Where(sm => sm.Member_RollNum == UserSession.Instance.LoggedInRollNumber)
                                      .Select(sm => sm.Society_Name)
                                      .ToList();

            notifications = context.Announcements
                         .Where(a => a.UserType == "Admin" || user_societies.Contains(a.PostedBySocietyName))
                         .ToList();
        }

        public void OnGet()
        {
            populate_UserData();
            populate_notifications();

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
                populate_UserData();
                populate_notifications(); 
                return Page();
            }

            if (Request.Form.TryGetValue("notifications", out var notifications))
            {
                
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
    