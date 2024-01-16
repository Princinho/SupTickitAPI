namespace SupTickit.Domain
{
    public class Company:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Project> Projects { get; set; }
    }
}
