namespace SupTickit.API.DTOs
{
    public class UsersEditDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Username { get; set; }
        public string Lastname { get; set; }
        public int? CompanyId { get; set; }
    }
}
