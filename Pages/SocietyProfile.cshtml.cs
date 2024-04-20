using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocietySync.DBcontext;
using SocietySync.Models;

namespace SocietySync.Pages
{
    public class SocietyProfileModel : PageModel
    {

        public List<User> pending_Users; 
        public void OnGet()
        {

        }

        public void addDatato_textArea(List<User> Users)
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            var Utility = new Utility();
            var society_data = "";
            foreach (var user in Users)
            {
                society_data += Utility.AddUserData_to_ViewModel(user);
                string My_SocietyNmae = Request.Query["stringValue"];
                var membership = context.SocietyMemberships.FirstOrDefault(sm => sm.Member_RollNum == user.RollNum && sm.Society_Name == My_SocietyNmae);
                society_data += $"Role: {membership.Role}\n\n";
            }

            ViewData["ViewUserDetails"] = society_data;
        }

        public List<User> GetPendingUsers_ExsistingMembers()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            string My_SocietyName = Request.Query["stringValue"];

            pending_Users = context.SocietyMemberships
            .Where(sm => sm.Society_Name == My_SocietyName && sm.Role != "New_member")
            .Select(sm => sm.User)
            .ToList();

            return pending_Users;
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
            string selected_user_RollNum = Request.Form["pendingRequests"];
            if (!string.IsNullOrEmpty(selected_user_RollNum))
            {
                var context = UserSession.Instance.GetSocietySyncContext();
                var user = context.Users.Where(u => u.RollNum == selected_user_RollNum).FirstOrDefault();
                Utility utility = new Utility();

                string UserData = utility.AddUserData_to_ViewModel(user);

                @ViewData["ViewUserDetails"] = UserData;
            }

            ManageRequests_ButtonClick(); //Because Now I want manage Requests button click functionality
        }

        public SocietyMembership findMemeber(SocietySyncContext context)
        {
            string selected_user_RollNum = Request.Form["pendingRequests"];
            if (!string.IsNullOrEmpty(selected_user_RollNum))
            {
                string My_SocietyName = Request.Query["stringValue"];
                var membership = context.SocietyMemberships.Where(sm => sm.Member_RollNum == selected_user_RollNum && sm.Society_Name == My_SocietyName).FirstOrDefault();
                return membership;
            }
            return null;
        }

        public void acceptRequest_ButtonClick()
        {

            var context = UserSession.Instance.GetSocietySyncContext();
            var membership = findMemeber(context);
            membership.Role = "Accepted";
            context.SaveChanges();
            ManageRequests_ButtonClick(); //Because Now I want manage Requests button click functionality
        }

        public void RejectRequest_ButtonClick()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            var membership = findMemeber(context);
            context.SocietyMemberships.Remove(membership);
            context.SaveChanges(); 
            ManageRequests_ButtonClick();
        }

        public void ManageMembers_ButtonClick()
        {
            pending_Users = GetPendingUsers_ExsistingMembers();
            ViewMembers_ButtonClick(); //because i want functionality of this button here
            ViewData["ManageMembersPopup"] = true;
        }

        public void ViewMembers_ButtonClick()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            string My_SocietyName = Request.Query["stringValue"];
            List<User> Users = context.SocietyMemberships.Where(Sm => Sm.Society_Name == My_SocietyName && Sm.Role != "New_member")
                .Select(sm => sm.User).ToList();

            addDatato_textArea(Users);
        }

        public void SaveChanges_ButtonClick()
        {

            var context = UserSession.Instance.GetSocietySyncContext();
            string My_SocietyName = Request.Query["stringValue"];

            pending_Users = GetPendingUsers_ExsistingMembers();

            string Selected_Member_RollNum = Request.Form["MemberRollNumber"];
            string new_role = Request.Form["memberStatus"];

            var membership = context.SocietyMemberships.FirstOrDefault(sm => sm.Society_Name == My_SocietyName && sm.Member_RollNum == Selected_Member_RollNum && sm.Role != "New_Member");
            membership.Role = new_role;
            context.SaveChanges();
            ViewMembers_ButtonClick(); //because i want functionality of this button here
            ViewData["ManageMembersPopup"] = true;
        }

        public void removeMember_Changes()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (Request.Form.TryGetValue("MenuButton", out var MenuButton))
            {
                switch (MenuButton)
                {
                    case "ViewMembers":
                        ViewMembers_ButtonClick();
                        break;
                    case "ManageMembers":
                        ManageMembers_ButtonClick();
                        break;
                    case "ManageRequests":
                        ManageRequests_ButtonClick();
                        break;
                }
            }


            if (Request.Form.TryGetValue("viewRequestDetailsBtn", out var viewRequestDetailsBtn))
            {
                viewRequestDetails_ButtonClick();
            }

            if (Request.Form.TryGetValue("acceptRequestBtn", out var acceptRequestBtn))
            {
                acceptRequest_ButtonClick();
            }

            if (Request.Form.TryGetValue("RejectRequestBtn", out var RejectRequestBtn))
            {
                RejectRequest_ButtonClick();
            }

            if (Request.Form.TryGetValue("saveChangesBtn", out var saveChangesBtn))
            {
                SaveChanges_ButtonClick();
            }

             return Page();
        }
    }
}
