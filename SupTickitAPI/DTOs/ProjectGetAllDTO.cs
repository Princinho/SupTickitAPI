namespace SupTickitAPI.DTOs
{
    public class ProjectGetAllDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public string ProductReferenceName { get; set; }
    }
}
