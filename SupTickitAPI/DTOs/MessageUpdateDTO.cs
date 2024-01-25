namespace SupTickit.API.DTOs
{
    public class MessageUpdateDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
    }
}
