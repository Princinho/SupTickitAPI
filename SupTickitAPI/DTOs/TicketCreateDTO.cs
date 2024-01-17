using SupTickitAPI.Enums;

namespace SupTickitAPI.DTOs
{
    public class TicketCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public string ProductReference { get; set; }
        public PriorityEnum Priority { get; set; }
        public TicketStatusEnum Status { get; set; }
    }
}
