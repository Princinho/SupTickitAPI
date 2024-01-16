using Suptickit.Domain.Enums;

namespace SupTickit.Domain
{
    public class RoleAssignment:BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int UserId { get; set; }
        public RoleEnum RoleId { get; set; }

    }
}
