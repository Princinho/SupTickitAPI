using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Suptickit.Domain.Models
{
    public class Vehicle:BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string VIN { get; set; }
        public string PlateNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
    }
}
