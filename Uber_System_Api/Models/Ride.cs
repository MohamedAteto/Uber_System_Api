using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber_System_Api.Models
{
    public class Ride
    {

        [Key]
        public int Id { get; set; }
        [Required,MaxLength(150)]
        public string PickupLocation { get; set; }
        [Required, MaxLength(150)]
        public string DropOffLocation { get; set; }
        [Range(20, 5000)]
        public decimal Fare { get; set; }
        [Required]
        public string Status { get; set; }


        public int RiderId { get; set; }
        [ForeignKey(nameof(RiderId))]
        public User UserRider { get; set; }

        public int DriverId { get; set; }
        [ForeignKey(nameof(DriverId))]
        public User UserDriver { get; set; }


        public Payment Payment { get; set; }


        public Rating rating { get; set; }
    }
}
