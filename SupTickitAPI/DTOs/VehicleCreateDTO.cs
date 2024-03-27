using Suptickit.Domain.Models;

namespace SupTickit.API.DTOs
{
    public class VehicleCreateDTO
    {
        public int CustomerId { get; set; }
        public string VIN { get; set; }
        public string PlateNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
    }
}
