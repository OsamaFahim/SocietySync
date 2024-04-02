using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SocietySync.Models;
using System.Net.Sockets;

namespace SocietySync.DBcontext
{
    public class SocietySyncContext : DbContext
    {
        public SocietySyncContext(DbContextOptions<SocietySyncContext> options)
            : base(options)
        {
        }

        // DbSets for each model
        public DbSet<User> Users { get; set; }
      /*  public DbSet<Society> Societies { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<SocietyMembership> SocietyMemberships { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Notification> Notifications { get; set; }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Custom configurations go here

            // For example, to configure a composite key if needed:
            // modelBuilder.Entity<YourEntity>().HasKey(e => new { e.FirstKeyId, e.SecondKeyId });

            // Setup relationships and any configuration specific to your models
            // This is where you can use Fluent API to configure constraints, indexes, etc.
        }
    }
}