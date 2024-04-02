using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SocietySync.Models
{
    public class User_Model : DbContext
    {
        public User_Model() : base("name=Model")
        {
        }

        public DbSet<User> Users { get; set; }

        // Uncomment if Society_Table is an entity with a relationship to User
        // public DbSet<Society_Table> SocietyTables { get; set; }

      /*  public void DeleteAllUsers()
        {
            var allUsers = Users.ToList();
            Users.RemoveRange(allUsers);
            SaveChanges();
        }*/
    }

    public class User
    {
        [Key]
        public string RollNum { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        // Uncomment if there are relationships with other entities
        // public virtual ICollection<Society_Table> SocietyTables { get; set; }
    }

    // Uncomment to define Society_Table entity
    /*
    public class Society_Table
    {
        [Key]
        public int Id { get; set; }

        // Other properties
        
        public virtual ICollection<User> Users { get; set; }
    }
    */
}
