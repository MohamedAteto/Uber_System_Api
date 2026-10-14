using System.ComponentModel.DataAnnotations;

namespace Uber_System_Api.DTOs.VehicleDTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string VehiclePlateNumber { get; set; }
        public string VehicleColor { get; set; }
        public int DriverId { get; set; }
    }
}
