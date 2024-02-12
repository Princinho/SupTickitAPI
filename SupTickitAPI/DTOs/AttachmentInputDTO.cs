using Suptickit.Domain.Enums;
using SupTickit.Domain;

namespace SupTickitAPI.DTOs
{
    public class AttachmentInputDTO
    {
        public IFormFile FileDetails { get; set; }
        public FileType FileType { get; set; }
        public int TicketId { get; set; }

    }
}
