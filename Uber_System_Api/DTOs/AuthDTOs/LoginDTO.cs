using System.ComponentModel.DataAnnotations;

namespace Uber_System_Api.DTOs.AuthDTOs
{
    public class LoginDTO
    {
        public string UserFullName { get; set; }
 
        public string Password { get; set; }

    }
}
