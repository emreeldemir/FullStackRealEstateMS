namespace RealEstate.API.DTOs.User
{
    public class UserLoginRequestDTO
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
