using Microsoft.EntityFrameworkCore;
using SocietySync.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SocietySync.Models
{
    public class Event
    {
        [Required]
        public string Name { get; set; }    

        [Required]
        public string Society_Name { get; set; } // Foreign Key

        [Required]
        public string Type { get; set; }    

        [Required]
        public DateTime Date { get; set; } 

        [Required]
        public string Description { get; set; }

        // Additional property to represent Date in DD/MM/YY format
        public string FormattedDate => Date.ToString("dd/MM/yyyy");

        // Navigation property to link back to the Society
        public virtual Society Society { get; set; }
    }
}
