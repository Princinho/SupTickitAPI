namespace SupTickit.API.DTOs
{
    public class PartGetAllDTO
    {
        public int Id { get; set; }
        public int PartCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLineEditAllowed { get; set; }
        public double Price { get; set; }
    }
}
