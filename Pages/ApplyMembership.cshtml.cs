using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SocietySync.Pages
{
    public class ApplyMembershipModel : PageModel
    {

        [BindProperty]
        public List<Society> societyList { get; set; }

        public void populateData()
        {
            var context = UserSession.Instance.GetSocietySyncContext();
            societyList = context.Societies.Where(s => s.Status == true).ToList();
        }

        public void OnGet()
        {
            populateData();   
        }

        public void AllSocities_buttonClick()
        {

            var context = UserSession.Instance.GetSocietySyncContext();
            List<Society> Societies = context.Societies.Where(s => s.Status == true).ToList();

            var Utility = new Utility();
            var society_data = "";
            foreach (var society in  Societies)
            {
                society_data += Utility.AddSocietyData_to_ViewModel(society);
            }

            ViewData["Selected_SocietyData"] = society_data;
        }

        public void CurrentSocieties_buttonClick()
        {

        }

        public void Register_buttonClick()
        {
            populateData();
            ViewData["ShowRegisterPopup"] = true;
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
                }
            }
            return Page();
        }
    }
}
