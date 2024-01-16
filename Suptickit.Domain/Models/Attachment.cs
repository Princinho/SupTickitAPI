namespace SupTickit.Domain
{
    public class Attachment:BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }
}
