using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber_System_Api.Models
{

    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Range(20, 5000)]
        public decimal Amount { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public string Status { get; set; }


        public int RideId { get; set; }
        [ForeignKey(nameof(RideId))]
        public Ride Ride { get; set; }


    }
}
