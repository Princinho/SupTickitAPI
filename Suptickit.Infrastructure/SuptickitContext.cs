using Microsoft.EntityFrameworkCore;
using Suptickit.Domain.Models;
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
            modelBuilder.Entity<Vehicle>().HasIndex(u => u.VIN).IsUnique();
            modelBuilder.Entity<Vehicle>().HasIndex(u => u.PlateNumber).IsUnique();
        }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<PartCategory> PartsCategories { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteDetail> QuoteDetail { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TaxOrBonus> TaxOrBonuses { get; set; }
        public DbSet<Ticket> Tickets{ get; set; }
        public DbSet<TicketLog> TicketLogs{ get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleAssignment> RoleAssignments { get; set; }
        public DbSet<Settings> Settings { get; set; }

    }
}
