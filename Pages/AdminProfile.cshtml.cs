using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocietySync.Models;
using SocietySync.DBcontext;
using System.Net.Sockets;

namespace SocietySync.Pages
{
    public class AdminControllerModel : PageModel
    {
        [BindProperty]
        public string announcementText{ get; set; }

        public long UserCount { get; set; }
        public List<Society> Pending_Societies { get; set; }

        public List<User> Remove_Users { get; set; }

        public List<Society> Remove_Societies { get; set; }

        public void update_Users_count()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            UserCount = context.Users.Count();
        }

        public void PopulateData_Pending_Societies()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            Pending_Societies = context.Societies.Where(s => !s.Status).ToList();
        }
        public void PopulateData_Remove_Societies()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            Remove_Societies = context.Societies.Where(s => s.Status == true).ToList();
        }

        public void PopulateData_Remove_Users()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            Remove_Users = context.Users.ToList();
        }

        public void OnGet()
        {
            update_Users_count();
        }

        public void ManageRequests_ButtonClick()
        {
            PopulateData_Pending_Societies();
            ViewData["manageRegistrationPopup"] = true;
        }

        public void MakeAnnoucement_ButtonClick()
        {
            ViewData["makeAnnoucementsPopup"] = true;
        }

        public void RemoveSocieties_ButtonClick()
        {
            ViewData["RemoveSocietiesPopup"] = true;
            PopulateData_Remove_Societies();
            ViewSocieties_ButtonClick();
        }

        public void RemoveUsers_ButtonClick()
        {
            ViewData["RemoveMembersPopup"] = true;
            PopulateData_Remove_Users();
            ViewUsers_ButtonClick();
        }

        public void submitAnnoucment_ButtonClick()
        {
            ModelState.Clear();
            string content = Request.Form["announcementText"];
            if (string.IsNullOrEmpty(content)) {
                MakeAnnoucement_ButtonClick();
                return; 
            }

            var context = UserSession.Instance.GetSocietySyncContext();
            var newAnnouncement = new Announcement
            {
                Content = content,
                UserType = "Admin"
            };
            context.Announcements.Add(newAnnouncement);
            context.SaveChanges();
            MakeAnnoucement_ButtonClick();
        }

        public void ViewUsers_ButtonClick()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            List<User> users = context.Users.ToList();

            string users_data = "";
            Utility utility = new Utility();
            foreach (var user in users)
            {
                users_data += utility.AddUserData_to_ViewModel(user);
                
            }

            ViewData["Selected_Data"] = users_data;
        }

        public void ViewSocieties_ButtonClick()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            List<Society> societies = context.Societies.Where(s => s.Status == true).ToList();

            string societies_data = "";
            Utility utility = new Utility();
            foreach (var society in societies)
            {
                societies_data += utility.AddSocietyData_to_ViewModel(society);

            }

            ViewData["Selected_Data"] = societies_data;

        }

        public void removeSociety_DB(string society_name)
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            Society society = context.Societies.FirstOrDefault(s => s.Name == society_name);
            context.Societies.Remove(society);
            context.SocietyMemberships.RemoveRange(context.SocietyMemberships.Where(sm => sm.Society_Name == society.Name));
            context.Events.RemoveRange(context.Events.Where(e => e.Society_Name == society.Name));
            context.SaveChanges();
        }

        public void RemoveSociety_ButtonClick()
        {
            RemoveSocieties_ButtonClick();

            string selected_society_name = Request.Form["RemoveSocieties_Select"];
            if (!string.IsNullOrEmpty(selected_society_name))
            {
                removeSociety_DB(selected_society_name);
            }

            RemoveSocieties_ButtonClick();
        }

        public void RemoveMember_ButtonClick()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            string selected_user_RollNum = Request.Form["RemoveUsers_Select"];
            if (!string.IsNullOrEmpty(selected_user_RollNum))
            {
                List<Society> societies = context.Societies.Where(s => s.PresidentRollNum == selected_user_RollNum).ToList();
                foreach (var society in societies)
                {
                    string society_name = society.Name;
                    removeSociety_DB(society_name);
                }

                var user = context.Users.FirstOrDefault(u => u.RollNum == selected_user_RollNum);
                context.Users.Remove(user);
                context.Announcements.RemoveRange(context.Announcements.Where(a => a.PostedByUserId == user.RollNum));
                context.SaveChanges();
            }

            RemoveUsers_ButtonClick();
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
                    case "ViewSocieties":
                        ViewSocieties_ButtonClick();
                        break;
                    case "ViewUsers":
                        ViewUsers_ButtonClick();
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

                        ViewData["Selected_Data"] = Selected_Society_Data;

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
                PopulateData_Pending_Societies();
            }

            if (Request.Form.TryGetValue("submitAnnouncementBtn", out var submitAnnouncementBtn))
            {
                submitAnnoucment_ButtonClick();
            }

            if (Request.Form.TryGetValue("RemoveMember_btn", out var RemoveMember_btn))
            {
                RemoveMember_ButtonClick();   
            }

            if (Request.Form.TryGetValue("RemoveSociety_btn", out var RemoveSociety_btn))
            {
                RemoveSociety_ButtonClick();
            }

            update_Users_count();
            return Page();
        }
    }
}
