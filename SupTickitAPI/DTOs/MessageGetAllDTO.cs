namespace SupTickit.API.DTOs
{
    public class MessageGetAllDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
