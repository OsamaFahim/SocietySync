using SocietySync.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SocietySync.Models
{
    public class SocietyMembership
    {
        [Key] 
        public string Member_RollNum { get; set; }

        [ForeignKey("Member_RollNum")]
        public User Member { get; set; }

        public string Society_Name { get; set; }

        [ForeignKey("Society_Name")]
        public string Society { get; set; }

        public string Role { get; set; } = "New_member";
    }

}
