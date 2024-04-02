using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using SocietySync.Models;

namespace SocietySync.Pages
{
    public class SignUpModel : PageModel
    {
        // Properties corresponding to form fields
        [BindProperty]
        public string RollNumber_input { get; set; }

        [BindProperty]
        public string Name_input { get; set; }
    
        [BindProperty]
        public string Password_input { get; set; }

        [BindProperty]
        public string ConfirmPassword_input { get; set; }

        [BindProperty]
        public bool ShowPassword { get; set; }

        public void OnGet()
        {
            ShowPassword = false;
        }

        private bool ValidateRollNumber(string rollNumber)
        {
            //Remove any extra spaces or things like that
            rollNumber = rollNumber.Trim();

            // Define the regular expression pattern for roll number validation
            string pattern = @"^[LKPFIT](1[1-9]|2[0-4])(-)(\d+)$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // Check if the input roll number matches the pattern
            if (regex.IsMatch(rollNumber))
            {
                // Extract the year part from the roll number
                string yearPart = regex.Match(rollNumber).Groups[1].Value;

                // Convert the extracted year to an integer
                int year;
                if (int.TryParse(yearPart, out year))
                {
                    // Get the current year
                    int currentYear = DateTime.Now.Year % 100; // For the year 2024

                    // Check if the extracted year is less than or equal to the current year
                    if (year <= currentYear)
                    {
                        return true; // Roll number is valid
                    }
                }
            }

            return false;
        }

        private bool ValidatePassword(string pass)
        {
            // Check if the password is at least 8 characters long
            if (pass.Length < 8)
            {
                return false; // Password is too short
            }

            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasSpecialChar = false;

            // Define the set of special characters
            string specialCharacters = "!@#$%^&*()-_=+[{]};:'\",<.>/?";

            // Check each character in the password
            foreach (char c in pass)
            {
                if (char.IsUpper(c))
                {
                    hasUppercase = true;
                }
                else if (char.IsLower(c))
                {
                    hasLowercase = true;
                }
                else if (specialCharacters.Contains(c))
                {
                    hasSpecialChar = true;
                }

                // If all required conditions are met, no need to continue checking
                if (hasUppercase && hasLowercase && hasSpecialChar)
                {
                    break;
                }
            }

            // Check if all required conditions are met
            return hasUppercase && hasLowercase && hasSpecialChar;
        }

        public IActionResult OnPost()
        {
            if (!ValidateRollNumber(RollNumber_input))
            {
                ModelState.AddModelError(nameof(SignUpModel.RollNumber_input), "The Roll Number must be of the Format {City}{Year}-{Roll-Num}");
                return Page();
            } 
            
            if(!ValidatePassword(Password_input))
            {
                ModelState.AddModelError(nameof(SignUpModel.Password_input), "Passwords must contain 8 charachters");
                return Page();
            }
            
            if(ConfirmPassword_input != Password_input)
            {
                ModelState.AddModelError(nameof(SignUpModel.Password_input), "Passwords do not match");
                return Page();
            }


            string hashedPassword = Password_Hasher.HashPassword(Password_input);

            return RedirectToPage("/Index");
        }
    }
}
