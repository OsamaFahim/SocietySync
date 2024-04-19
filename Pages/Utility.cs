using Azure.Core;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SocietySync.Pages
{
    public class Utility
    {
        public string AddSocietyData_to_ViewModel(Society SelectedSociety)
        {
            var Selected_Society_Data = $"Soceity Name: {SelectedSociety.Name}\n\n";
            Selected_Society_Data += $"Requester Roll-Num: {SelectedSociety.PresidentRollNum}\n\n";
            Selected_Society_Data += $"Category: {SelectedSociety.Category}\n\n";
            Selected_Society_Data += $"Goals/Objectives: {SelectedSociety.Goals}\n\n";
            Selected_Society_Data += $"Social Media: {SelectedSociety.Link}\n\n";


            return Selected_Society_Data;
        }
    }
}
