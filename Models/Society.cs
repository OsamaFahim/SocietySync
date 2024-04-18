using SocietySync.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Society
{
    [Key]
    public string Name { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public string Goals { get; set; }

    public string Link { get; set; }  //A society currently might not be on social media

    [Required]
    public string PresidentRollNum { get; set; }

    [ForeignKey("PresidentRollNum")]
    public User President { get; set; }

    public bool Status { get; set; } = false; 
}
