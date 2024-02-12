namespace SupTickit.API.DTOs
{
    public class TicketCategoryOutDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int ProjectId { get; set; }
    }
}
