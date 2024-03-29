﻿namespace SupTickit.Domain
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Lastname { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public List<RoleAssignment>? RoleAssignments { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
    }
}
