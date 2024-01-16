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
        
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
