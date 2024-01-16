using SupTickitAPI.Enums;

namespace SupTickit.Domain

{
    public class TicketLog:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AssignedBy { get; set; }
        public int AgentId { get; set; }
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }
        public string ProductReference { get; set; }
        public PriorityEnum Priority{ get; set; }
    }
}
