using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocietySync.Models;
using SocietySync.DBcontext;

namespace SocietySync.Pages
{
    public class AdminControllerModel : PageModel
    {
        public long UserCount { get; set; }
        public List<Society> Pending_Societies { get; set; }

        [BindProperty]
        public string pendingRequests { get; set; }

        public void update_Users_count()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            UserCount = context.Users.Count();
        }

        public void PopulateData()
        {
            var context = UserSession.Instance.GetSocietySyncContext();


            UserCount = context.Users.Count();
            Pending_Societies = context.Societies.Where(s => !s.Status).ToList();
        }

        public void OnGet()
        {
            update_Users_count();
        }

        public void ManageRequests_ButtonClick()
        {
            PopulateData();
            ViewData["manageRegistrationPopup"] = true;
        }

        public void MakeAnnoucement_ButtonClick()
        {
            ViewData["makeAnnoucementsPopup"] = true;
        }

        public void RemoveSocieties_ButtonClick()
        {
            ViewData["RemoveSocietiesPopup"] = true;
        }

        public void RemoveUsers_ButtonClick()
        {
            ViewData["RemoveMembersPopup"] = true;
        }

        public void submitAnnoucment_buttonClick()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            string content = Request.Form["announcementText"];
            var newAnnouncement = new Announcement
            {
                Content = content,
                UserType = "Admin"
            };
            context.Announcements.Add(newAnnouncement);
            context.SaveChanges();
            ViewData["makeAnnouncementPopup"] = true;
        }

        public async Task<IActionResult> OnPost()
        {
            var context = UserSession.Instance.GetSocietySyncContext();

            if (Request.Form.TryGetValue("MenuButton", out var MenuButton))
            {
                switch (MenuButton)
                {
                    case "ManageRequests":
                        ManageRequests_ButtonClick();
                        break;
                    case "MakeAnnoucement":
                        MakeAnnoucement_ButtonClick();
                        break;
                    case "RemoveSocieties":
                        RemoveSocieties_ButtonClick();
                        break;
                    case "RemoveUsers":
                        RemoveUsers_ButtonClick();  
                        break;

                }
            }

            if (Request.Form.TryGetValue("DetailsBtn", out var detailsButtonValue_temp) ||
                Request.Form.TryGetValue("AcceptBtn", out var AcceptButtonValue_temp) ||
                Request.Form.TryGetValue("RejectBtn", out var RejectButtonValue_temp))
            {
                if (!string.IsNullOrEmpty(Request.Form["pendingRequests"]))
                {
                    if (Request.Form.TryGetValue("DetailsBtn", out var detailsButtonValue))
                    {
                        string selectedPendingSocietyName = Request.Form["pendingRequests"];
                        var SelectedSociety = context.Societies.FirstOrDefault(s => s.Name == selectedPendingSocietyName);

                        Utility utility = new Utility();
                        var Selected_Society_Data = utility.AddSocietyData_to_ViewModel(SelectedSociety);

                        ViewData["Selected_SocietyData"] = Selected_Society_Data;

                    }
                    else if (Request.Form.TryGetValue("AcceptBtn", out var AcceptButtonValues))
                    {
                        string selectedPendingSocietyName = Request.Form["pendingRequests"];
                        var selectedSociety = context.Societies.FirstOrDefault(s => s.Name == selectedPendingSocietyName);
                        selectedSociety.Status = true;
                        context.SaveChanges();
                    }
                    else if (Request.Form.TryGetValue("RejectBtn", out var RejectButtonValue))
                    {
                        string selectedPendingSocietyName = Request.Form["pendingRequests"];
                        var selectedSociety = context.Societies.FirstOrDefault(s => s.Name == selectedPendingSocietyName);
                        context.Remove(selectedSociety);
                        context.SaveChanges();
                    }
                }

                ViewData["manageRegistrationPopup"] = true;
                PopulateData();
            }

            if ((Request.Form.TryGetValue("submitAnnouncementBtn", out var submitAnnouncementBtn)))
            {
                submitAnnoucment_buttonClick();
            }

            update_Users_count();
            return Page();
        }
    }
}
