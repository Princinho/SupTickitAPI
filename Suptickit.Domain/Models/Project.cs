namespace SupTickit.Domain
{
    public class Project:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TicketCategory> Categories { get; set; }
        public List<Company> Companies { get; set; }
        public List<Ticket>Tickets { get; set; }
    }
}
