namespace SupTickit.API.DTOs
{
    public class QuoteDetailUpdateDTO
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public int PartId { get; set; }
        public double Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double TotalPrice { get; set; }
    }
}
