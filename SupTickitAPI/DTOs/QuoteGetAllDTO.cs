namespace SupTickit.API.DTOs
{
    public class QuoteGetAllDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Total { get; set; }
        public double TotalWithTaxOrBonuses { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public IEnumerable<QuoteDetailGetAllDTO> QuoteDetails { get; set; }
        public IEnumerable<TaxOrBonusAppliedGetDTO> TaxOrBonusesApplied { get; set; }
    }
}
