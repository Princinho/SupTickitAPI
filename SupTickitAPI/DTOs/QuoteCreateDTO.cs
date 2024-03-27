using Suptickit.Domain.Models;

namespace SupTickit.API.DTOs
{
    public class QuoteCreateDTO
    {
        
        public string ReferenceNumber { get; set; }
        public double Mileage { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public IEnumerable<QuoteDetailCreateDTO> QuoteDetails { get; set; }
    }
}
