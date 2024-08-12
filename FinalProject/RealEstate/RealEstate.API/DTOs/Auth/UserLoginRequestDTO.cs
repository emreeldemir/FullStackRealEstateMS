namespace RealEstate.API.DTOs.Auth
{
    public class UserLoginRequestDTO
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
