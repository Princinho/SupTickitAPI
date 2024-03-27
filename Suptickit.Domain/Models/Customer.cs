using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Domain.Models
{
    public class Customer : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Phone { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<Quote> Quotes { get; set; } = Enumerable.Empty<Quote>();
    }
}
