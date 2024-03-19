using Suptickit.Domain.Enums;

namespace SupTickit.Domain
{
    public class Attachment:BaseEntity
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public FileType FileType { get; set; }

    }
}
