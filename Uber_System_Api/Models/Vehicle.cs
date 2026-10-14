using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber_System_Api.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        [Required , MaxLength(20)]
        public string VehiclePlateNumber { get; set; }
        [Required]
        public string VehicleColor { get; set; }


        public int DriverId { get; set; }
        public User? Driver { get; set; }

    }
}
