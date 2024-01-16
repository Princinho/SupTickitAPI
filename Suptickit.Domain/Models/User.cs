namespace SupTickit.Domain
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int CompanyId { get; set; }
        public List<RoleAssignment> RoleAssignments { get; set; }

    }
}
