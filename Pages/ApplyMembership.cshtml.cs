using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocietySync.Models;

namespace SocietySync.Pages
{
    public class ApplyMembershipModel : PageModel
    {

        public List<Society> societyList { get; set; }

        public void populateData(string button_type)
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            if (button_type == "Register")
            {
                societyList = context.Societies
               .Where(s => s.Status == true &&
                    s.PresidentRollNum != UserSession.Instance.LoggedInRollNumber &&
                    !context.SocietyMemberships.Any(m => m.Society_Name == s.Name && m.Member_RollNum == UserSession.Instance.LoggedInRollNumber)).ToList();
            } else
            {
                societyList = context.Societies
                .Where(s => s.Status == true && s.PresidentRollNum == UserSession.Instance.LoggedInRollNumber).ToList();
            }
        }


        public void addDatato_textArea(List<Society> Societies)
        {
            var Utility = new Utility();
            var society_data = "";
            foreach (var society in Societies)
            {
                society_data += Utility.AddSocietyData_to_ViewModel(society);
            }

            ViewData["Selected_SocietyData"] = society_data;
        }
        public void AllSocities_buttonClick()
        {

            var context = UserSession.Instance.GetSocietySyncContext();
            List<Society> Societies = context.Societies.Where(s => s.Status == true).ToList();

            addDatato_textArea(Societies);
        }

        public void CurrentSocieties_buttonClick()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            List<Society> Societies = context.SocietyMemberships
            .Where(sm => sm.Member_RollNum == UserSession.Instance.LoggedInRollNumber && sm.Role != "New_member")
            .Select(sm => sm.Society) .ToList();
            
            addDatato_textArea(Societies) ;
    
        }

        public void Register_buttonClick()
        {
            populateData("Register");
            ViewData["ShowRegisterPopup"] = true;
        }

        public void ApplyMembership_buttonClick()
        {
            string selectedSocietyName = Request.Form["SelectedSociety"];
            if (!string.IsNullOrEmpty(selectedSocietyName))
            {
                var context = UserSession.Instance.GetSocietySyncContext();

                //newMembership is being added to the table
                var newMembership = new SocietyMembership
                {
                    Member_RollNum = UserSession.Instance.LoggedInRollNumber,
                    Society_Name = selectedSocietyName,
                };

                var user = context.Users.FirstOrDefault(u => u.RollNum == UserSession.Instance.LoggedInRollNumber);
                var society = context.Societies.FirstOrDefault(s => s.Name == selectedSocietyName);

                newMembership.User = user;
                newMembership.Society = society;


                context.SocietyMemberships.Add(newMembership);
                context.SaveChanges();
            }

            Register_buttonClick();     //Becuase Here I want to have Register Button click Functionality
        }

        public void MySocieties_buttonClick()
        {
            populateData("MySocieties");
            ViewData["MySocietiesPopup"] = true;
        }

        public async Task<IActionResult> OnPost()
        {
            if (Request.Form.TryGetValue("MenuButton", out var MenuButton))
            {   
                switch (MenuButton)
                {
                    case "AllSocieties":
                        AllSocities_buttonClick();
                        break;
                    case "CurrentSocieties":
                        CurrentSocieties_buttonClick();
                        break;
                    case "Register":
                        Register_buttonClick();
                        break;
                    case "MySocietiesBtn":
                        MySocieties_buttonClick();
                        break;
                }
            }

            if (Request.Form.TryGetValue("Upper_MenuButton", out var Upper_Menu))
            {
                switch (Upper_Menu)
                {
                    case "Home":
                       return RedirectToPage("/Index");
                    case "Events":
                        return RedirectToPage("/Home");
                }
            }

            if (Request.Form.TryGetValue("applyMembershipBtn", out var applyMembershipBtn))
            {
                ApplyMembership_buttonClick();
            }

            if (Request.Form.TryGetValue("MySocietyBtn", out var MySocietyBtn))
            {
                string selectedSocietyName = Request.Form["SelectedSociety_My"];
                if (!string.IsNullOrEmpty(selectedSocietyName)) {
                   return RedirectToPage("/SocietyProfile", new { stringValue = selectedSocietyName});
                }
            }

            return Page();
        }
    }
}
