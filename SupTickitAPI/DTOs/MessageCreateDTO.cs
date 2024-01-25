namespace SupTickit.API.DTOs
{
    public class MessageCreateDTO
    {
        public string Body { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
    }
}
