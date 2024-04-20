using SocietySync.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SocietySync.Models
{
    public class SocietyMembership
    {
        [Required]
        public string Member_RollNum { get; set; } //Foreign key

        [Required]
        public string Society_Name { get; set; }  //Foreign key

        public string Role { get; set; } = "New_member";

        //Navigation Properties
        public virtual User User { get; set; }
        public virtual Society Society { get; set; }
    }

}
