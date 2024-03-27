namespace SupTickit.API.DTOs
{
    public class QuoteUpdateDTO
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public IEnumerable<QuoteDetailUpdateDTO> QuoteDetails { get; set; }
    }
}
