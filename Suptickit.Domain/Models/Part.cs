using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Domain.Models
{
    public class Part:BaseEntity
    {
        public int PartCategoryId { get; set; }
        public PartCategory PartCategory { get; set; }
        public IEnumerable<QuoteDetail> QuoteDetails { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLineEditAllowed { get; set; }
        public double Price { get; set; }
    }
}
