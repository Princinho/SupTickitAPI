using SupTickitAPI.Enums;

namespace SupTickit.API.DTOs
{
    public class TicketLogDTO
    {
        public int Type { get; set; }
        public DateTime LogDate { get; set; }
        public string? AttachmentLink{ get; set; }
        public int? AttachmentId{ get; set; }
        public string? MessageContent { get; set; }
        public string? TicketName { get; set; }
        public string? MessageBody { get; set; }
        public int MessageId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int AssignedBy { get; set; }
        public int AgentId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductReference { get; set; }
        public PriorityEnum Priority { get; set; }
        public TicketStatusEnum Status { get; set; }
    }
}
