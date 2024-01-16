using SupTickit.Domain;

namespace SupTickitAPI.DTOs
{
    public class ProjectWithCompaniesInputDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> CompanyIds { get; set; }
    }
}
