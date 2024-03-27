using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Domain.Models
{
    public class TaxOrBonusApplied:BaseEntity
    {
        public int TaxOrBonusId { get; set; }
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
        public TaxOrBonus TaxOrBonus { get; set; }
        public double Amount { get; set; }
    }
}
