using System.ComponentModel.DataAnnotations;

namespace Uber_System_Api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(100 , MinimumLength = 3)]
        public string UserFullName { get; set; }
        [Required , EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required , Phone]

        public string PhoneNumber { get; set; }
        [Required]

        public string Role { get; set; } 


        public Vehicle? Vehicle { get; set; }

        public ICollection<Ride> Rides { get; set; } = new LinkedList<Ride>();
        public ICollection<Ride> DriverRides { get; set; } = new LinkedList<Ride>();
    }
}
