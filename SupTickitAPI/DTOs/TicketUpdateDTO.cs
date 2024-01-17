using SupTickitAPI.Enums;

namespace SupTickit.API.DTOs
{
    public class TicketUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public string ProductReference { get; set; }
        public PriorityEnum Priority { get; set; }
        public TicketStatusEnum Status { get; set; }
    }
}
