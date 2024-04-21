using Microsoft.EntityFrameworkCore;
using SocietySync.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocietySync.Models
{
    public class Announcement
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto incrment
        public int Id { get; set; } // Primary Key

        public string? PostedByUserId { get; set; } // Foreign Key

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserType { get; set; }

        // Navigation properties
        public virtual User PostedByUser { get; set; }
    }
}
