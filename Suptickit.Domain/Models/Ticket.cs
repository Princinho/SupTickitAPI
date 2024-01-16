using SupTickitAPI.Enums;

namespace SupTickit.Domain

{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int AssignedBy { get; set; }
        public int AgentId { get; set; }
        public int CategoryId { get; set; }
        public TicketCategory Category { get; set; }
        public List<Attachment> Attachments { get; set; }
        public string ProductReference { get; set; }
        public PriorityEnum Priority { get; set; }
        public TicketStatusEnum Status { get; set; }
    }
}
