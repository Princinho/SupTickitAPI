using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Domain.Models
{
    public class QuoteDetail:BaseEntity
    {
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
        public int PartId { get; set; }
        public Part Part { get; set; }
        public double Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double TotalPrice { get; set; }
    }
}
