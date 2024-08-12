namespace RealEstate.API.DTOs.Auth
{
    public class UserLoginResponseDTO
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
