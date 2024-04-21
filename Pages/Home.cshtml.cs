using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocietySync.DBcontext;
using SocietySync.Models;

namespace SocietySync.Pages
{
    public class HomeModel : PageModel
    {
        public void OnGet()
        {
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
    }
}
