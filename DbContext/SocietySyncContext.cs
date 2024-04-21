using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocietySync.Models;
using System.Net.Sockets;

namespace SocietySync.DBcontext
{
    public class SocietySyncContext : DbContext
    {
        public SocietySyncContext(DbContextOptions<SocietySyncContext> options)
            : base(options)
        {}

        // DbSets for each model
        public DbSet<User> Users { get; set; }
        public DbSet<Society> Societies { get; set; }
        public DbSet<SocietyMembership> SocietyMemberships { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<Announcement> Announcements { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SocietyMembership>()
                .HasKey(e => new { e.Member_RollNum, e.Society_Name })
                .HasName("ID");

            modelBuilder.Entity<Event>()
                .HasKey(e => new { e.Name, e.Society_Name })
                .HasName("Event_ID");
        }
    }
}
