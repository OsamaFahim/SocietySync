using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SocietySync.Models
{
    /*    public class User_Model : DbContext
        {
            public User_Model() : base("name=Model")
            {
            }

            public DbSet<User> Users { get; set; }

            // Uncomment if Society_Table is an entity with a relationship to User
            // public DbSet<Society_Table> SocietyTables { get; set; }

          *//*  public void DeleteAllUsers()
            {
                var allUsers = Users.ToList();
                Users.RemoveRange(allUsers);
                SaveChanges();
            }*//*
        }*/

    public class User
    {
        [Key]
        public string RollNum { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Hashed_Password { get; set; }

        public virtual ICollection<Society> PresidentSocieties { get; set; }

        public virtual ICollection<SocietyMembership> SocietyMemberships { get; set; }
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
