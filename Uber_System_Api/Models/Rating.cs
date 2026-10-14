using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber_System_Api.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [Range(1,5)]
        public decimal Score { get; set; }


        public int RideId { get; set; }
        [ForeignKey("RideId")]
        public Ride Ride { get; set; }


    }
}
