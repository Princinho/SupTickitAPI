using System.ComponentModel;

namespace SupTickit.Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
