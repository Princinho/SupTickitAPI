using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Domain.Models
{
    public class Quote:BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string ReferenceNumber { get; set; }
        public double Total { get; set; }
        public double TotalWithTaxOrBonuses { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } 
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public IEnumerable<QuoteDetail> QuoteDetails { get; set; }
        public IEnumerable<TaxOrBonusApplied> TaxOrBonusesApplied { get; set; } = new List<TaxOrBonusApplied>();
    }
}
