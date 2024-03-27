using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Domain.Models
{
    public class TaxOrBonus:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsBonus { get; set; }
        public double Amount { get; set; }
        public bool IsPercentage { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsArchived { get; set; }
        public IEnumerable<TaxOrBonusApplied> AppliedValues { get; set; }
        public string? ExclusionList { get; set; }
    }
}
