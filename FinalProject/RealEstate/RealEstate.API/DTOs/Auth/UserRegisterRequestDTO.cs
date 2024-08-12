using System.ComponentModel.DataAnnotations;

namespace RealEstate.API.DTOs.Auth
{
    public class UserRegisterRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
    }
}
