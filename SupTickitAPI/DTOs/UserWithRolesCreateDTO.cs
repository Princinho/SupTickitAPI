using SupTickit.Domain;

namespace SupTickit.API.DTOs
{
    public class UserWithRolesCreateDTO
    {
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? CompanyId { get; set; }
        public string PasswordConfirmation { get; set; }
        public List<RoleAssignment> RoleAssignments { get; set; }
    }
}
