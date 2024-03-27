using Suptickit.Domain.Models;
using SupTickit.Domain;

namespace SupTickit.API.DTOs
{
    public class QuoteDetailCreateDTO
    {
        
            public int PartId { get; set; }
            public double Quantity { get; set; }
            public double PricePerUnit { get; set; }
            public double TotalPrice { get; set; }
        
    }
}
