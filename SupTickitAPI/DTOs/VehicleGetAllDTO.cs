

namespace SupTickit.API.DTOs
{
    public class VehicleGetAllDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public CustomerGetAllDTO Customer { get; set; }
        public string VIN { get; set; }
        public string PlateNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
    }
}
