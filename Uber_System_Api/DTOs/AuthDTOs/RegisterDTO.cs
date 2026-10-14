using System.ComponentModel.DataAnnotations;

namespace Uber_System_Api.DTOs.AuthDTOs
{
    public class RegisterDTO
    {
        public string UserFullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

    }
}
