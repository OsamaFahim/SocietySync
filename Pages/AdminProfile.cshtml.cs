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

        public void PopulateData()
        {
            var context = UserSession.Instance.GetSocietySyncContext();


            UserCount = context.Users.Count();
            Pending_Societies = context.Societies.Where(s => !s.Status).ToList();
        }

        public void OnGet()
        {
           PopulateData();
        }

        public async Task<IActionResult> OnPost()
        {
            var context = UserSession.Instance.GetSocietySyncContext();

            if (!string.IsNullOrEmpty(Request.Form["pendingRequests"]))
            {
                if (Request.Form.TryGetValue("DetailsBtn", out var detailsButtonValue))
                {
                    string selectedPendingSocietyName = Request.Form["pendingRequests"];
                    var SelectedSociety = context.Societies.FirstOrDefault(s => s.Name == selectedPendingSocietyName);

                    var Selected_Society_Data = $"Soceity Name: {SelectedSociety.Name}\n\n";
                    Selected_Society_Data += $"Requester Roll-Num: {SelectedSociety.PresidentRollNum}\n\n";
                    Selected_Society_Data += $"Category: {SelectedSociety.Category}\n\n";
                    Selected_Society_Data += $"Goals/Objectives: {SelectedSociety.Goals}\n\n";
                    Selected_Society_Data += $"Social-Media Link: {SelectedSociety.Link}\n\n";


                    ViewData["Selected_SocietyData"] = Selected_Society_Data;

                }
                else if (Request.Form.TryGetValue("AcceptBtn", out var AcceptButtonValue))
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
            
            PopulateData();
            return Page();
        }

    }
}
