using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocietySync.DBcontext;
using SocietySync.Models;
using System.Linq;

namespace SocietySync.Pages
{
    public class HomeModel : PageModel
    {

        public List<Announcement> notifications;
        public void OnGet()
        {
            populate_notifications();
            var context = UserSession.Instance.GetSocietySyncContext();

            List<SocietyMembership> memberships = context.SocietyMemberships.Where(sm => sm.Member_RollNum == UserSession.Instance.LoggedInRollNumber && sm.Role != "New_member").ToList();
            List<string> societyNames = memberships.Select(m => m.Society_Name).ToList();
            List<Event> Events = context.Events.Where(e => societyNames.Contains(e.Society_Name)).ToList();

            var EventData = "";
            foreach (var Event in Events)
            {
                EventData += $"Event Name: {Event.Name}\n\n";
                EventData += $"Society Name: {Event.Society_Name}\n\n";
                EventData += $"Description : {Event.Description}\n\n";
                EventData += $"Event Date: {Event.FormattedDate}\n\n";
                EventData += $"Event Type: {Event.Type}\n\n";
            }

            ViewData["Events_View"] = EventData;
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
        public void notifications_ButtonClick()
        {
            
        }

        public async void onPost()
        {
            if (Request.Form.TryGetValue("Notifications_dropdown", out var Notifications_dropdown))
            {
                notifications_ButtonClick();
            }
        }
    }

}
