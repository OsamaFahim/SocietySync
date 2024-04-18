using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocietySync.DBcontext;
using System.Text.RegularExpressions;

namespace SocietySync.Pages
{
    public class SocietyRegisterationModel : PageModel
    {

        [BindProperty]
        public string name_input { get; set; }

        [BindProperty]
        public string category_input { get; set; }

        [BindProperty]
        public string goals_input { get; set; }

        [BindProperty]
        public string socialMedia_input { get; set; }

        public static bool IsValidSocialMediaLink(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            // Regular expression pattern for validating social media URLs
            string pattern = @"^(https?://)?(www\.)?(facebook\.com|twitter\.com|instagram\.com|linkedin\.com|youtube\.com)/.*$";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(url);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!IsValidSocialMediaLink(socialMedia_input))
            {
                ModelState.AddModelError(nameof(SocietyRegisterationModel.socialMedia_input), "Please Enter a Valid URL");
                return Page();
            }

            var context = UserSession.Instance.GetSocietySyncContext();

            if (context.Societies.Any(s => s.Name == name_input))
            {
                ModelState.AddModelError(nameof(SocietyRegisterationModel.name_input), "This name is already taken");
                return Page();
            }

            string Roll_Num = UserSession.Instance.LoggedInRollNumber;
            var President = context.Users.FirstOrDefault(u => u.RollNum == Roll_Num);

            var newSociety = new Society
            {
                Name = name_input,
                Category = category_input,
                Goals = goals_input,
                Link = socialMedia_input,
                President = President,   
            };

            context.Societies.Add(newSociety);

            await context.SaveChangesAsync();

            return RedirectToPage("/SocietyRegisteration");
        }
    }
}
