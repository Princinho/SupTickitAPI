using SupTickit.Domain;

namespace SupTickit.API.DTOs
{
    public class UsersGetAllDTO
    {
        public  int Id { get; set; }
        public string Firstname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Lastname { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int? CompanyId { get; set; }
        public CompanyGetAllDTO? Company { get; set; }
        public List<RoleAssignment>? RoleAssignments { get; set; }
    }
}
