namespace SupTickit.API.DTOs
{
    public class ChangePasswordDTO
    {
        public string Username { get; set; }
        public string   OldPassword{ get; set; }
        public  string Password { get; set; }
    }
}
