using SupTickit.Domain;

namespace SupTickitAPI.DTOs
{
    public class AttachmentInputDTO
    {
        public class Attachment : BaseEntity
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string Type { get; set; }
        }
    }
}
