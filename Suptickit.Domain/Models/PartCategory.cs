using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Domain.Models
{
    public class PartCategory:BaseEntity
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public IEnumerable<Part> Services { get; set; }
    }
}
