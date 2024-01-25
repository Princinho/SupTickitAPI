namespace SupTickit.API.DTOs
{
    public class UserRegisterDto
    {
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; }= String.Empty;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int CompanyId { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
