using Azure.Core;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SocietySync.Models;

namespace SocietySync.Pages
{
    public class Utility
    {
        public string AddSocietyData_to_ViewModel(Society SelectedSociety)
        {
            var Selected_Society_Data = $"Society Name: {SelectedSociety.Name}\n\n";
            Selected_Society_Data += $"Requester Roll-Num: {SelectedSociety.PresidentRollNum}\n\n";
            Selected_Society_Data += $"Category: {SelectedSociety.Category}\n\n";
            Selected_Society_Data += $"Goals/Objectives: {SelectedSociety.Goals}\n\n";
            Selected_Society_Data += $"Social Media: {SelectedSociety.Link}\n\n";

            return Selected_Society_Data;
        }

        public string AddUserData_to_ViewModel(User SelectedUser)
        {
            var Selected_User_Data = $"Name: {SelectedUser.Name}\n\n";
            Selected_User_Data += $"Roll-Num: {SelectedUser.RollNum}\n\n";
            Selected_User_Data += $"Email: {SelectedUser.RollNum + "@nu.edu.pk"}\n\n";

            return Selected_User_Data;
        }
    }
}
