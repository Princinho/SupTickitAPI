using Suptickit.Domain.Models;

namespace SupTickit.API.DTOs
{
    public class TaxOrBonusCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsBonus { get; set; }
        public double Amount { get; set; }
        public bool IsPercentage { get; set; }
        public string? ExclusionList { get; set; }
        public bool IsEnabled { get; set; }
    }
}
