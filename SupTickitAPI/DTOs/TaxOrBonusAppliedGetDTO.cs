
namespace SupTickit.API.DTOs
{
    public class TaxOrBonusAppliedGetDTO
    {
        public int TaxOrBonusId { get; set; }
        public int QuoteId { get; set; }
        public double Amount { get; set; }
    }
}
