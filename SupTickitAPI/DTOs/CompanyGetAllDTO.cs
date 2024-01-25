using SupTickit.Domain;
using SupTickitAPI.DTOs;

namespace SupTickit.API.DTOs
{
    public class CompanyGetAllDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProjectGetAllDTO> Projects { get; set; }
        public List<UsersGetAllDTO> Users { get; set; }
    }
}
