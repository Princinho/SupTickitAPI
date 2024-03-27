using Suptickit.Domain.Models;

namespace SupTickit.API.DTOs
{
    public class CustomerCreateDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Phone { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
    }
}
