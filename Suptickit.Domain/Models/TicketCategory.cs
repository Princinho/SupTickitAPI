namespace SupTickit.Domain
{
    public class TicketCategory:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
