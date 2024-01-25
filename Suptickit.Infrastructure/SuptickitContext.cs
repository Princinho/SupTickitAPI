using Microsoft.EntityFrameworkCore;
using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public class SuptickitContext : DbContext
    {
        public SuptickitContext(DbContextOptions<SuptickitContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<RoleAssignment>().Property(r=>r.RoleId).ValueGeneratedNever();
            //modelBuilder.Entity<RoleAssignment>().Property(r=>r.UserId).ValueGeneratedNever();
            //modelBuilder.Entity<RoleAssignment>().HasKey(r=>new {r.UserId,r.RoleId});
            
        }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<Ticket> Tickets{ get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleAssignment> RoleAssignments { get; set; }

    }
}
